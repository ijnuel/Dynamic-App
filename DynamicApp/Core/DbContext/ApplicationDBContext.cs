using Core.EntityInterfaces.Base;
using Core.EntityModels;
using Core.EntityModels.Base;
using Core.Helpers;
using Core.SessionIdentitier;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.AccessControl;

namespace Core.DbContext
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>, IApplicationDBContext
    {
        private readonly ISessionIdentifier _sessionIdentifier;
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options,
            ISessionIdentifier sessionIdentifier
            ) : base(options)
        {
            _sessionIdentifier = sessionIdentifier;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "p");
                    var deletedCheck = Expression.Lambda(Expression.Equal(Expression.Property(parameter, "IsArchived"), Expression.Constant(false)), parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(deletedCheck);
                }
            }
            modelBuilder.Entity<AppUser>()
                .Property(d => d.ConcurrencyStamp)
                .IsETagConcurrency();


            modelBuilder.Entity<IdentityRole>()
                .Property(d => d.ConcurrencyStamp)
                .IsETagConcurrency();

            
            
            modelBuilder.Entity<FormResponses>()
                .ToContainer("FormResponses");

            modelBuilder.Ignore<QuestionAnswer>();

            modelBuilder.Entity<FormResponses>()
                .OwnsMany(o => o.QuestionAnswers);



            modelBuilder.Entity<ProgramForm>()
                .ToContainer("ProgramForms");

            modelBuilder.Ignore<FormSection>();
            modelBuilder.Ignore<DynamicQuestion>();
            
            modelBuilder.Entity<ProgramForm>()
                .OwnsMany(
                o => o.Sections, 
                os =>
                {
                    os.OwnsMany(oso => oso.Questions);
                });

            base.OnModelCreating(modelBuilder);
        }



        public DbSet<FormResponses> FormResponses { get; set; }
        public DbSet<ProgramForm> ProgramForms { get; set; }



        public DbSet<TEntity>? GetDbSet<TEntity>() where TEntity : BaseEntity
        {
            return GetType().GetProperties()
                .FirstOrDefault(prop => prop?.PropertyType?.FullName?.Contains(typeof(TEntity)?.FullName) ?? false)?
                .GetValue(this) as DbSet<TEntity>;
        }

        public async Task<List<TEntity>> GetMany<TEntity>(Expression<Func<TEntity, bool>> query, int start = 0, int recordsPerPage = int.MaxValue, string searchText = "") where TEntity : BaseEntity
        {
            var _dbSet = GetDbSet<TEntity>();
            query = IncludeSearchColumnsQuery(query, searchText);

            return await _dbSet?.Where(query).Skip(start).Take(recordsPerPage).ToListAsync();
        }

        private static Expression<Func<TEntity, bool>> IncludeSearchColumnsQuery<TEntity>(Expression<Func<TEntity, bool>> query, string searchText) where TEntity : BaseEntity
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                Expression<Func<TEntity, bool>> searchQuery = y => false;
                var param = Expression.Parameter(typeof(TEntity), "p");
                var method = typeof(string).GetMethods().FirstOrDefault(x => x.Name == "Contains");
                var stringProperties = typeof(TEntity).GetProperties().Where(prop => prop.PropertyType == typeof(string)).ToList();
                foreach (var stringProperty in stringProperties)
                {
                    var call = Expression.Call(Expression.Property(param, stringProperty.Name), method, Expression.Constant(searchText));
                    var exp = Expression.Lambda<Func<TEntity, bool>>(call, param);
                    searchQuery = searchQuery.Or(exp);

                }
                query = query.And(searchQuery);
            }

            return query;
        }

        public async Task<TEntity?> GetOne<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity
        {
            var _dbSet = GetDbSet<TEntity>();
            return await _dbSet?.FirstOrDefaultAsync(query);
        }

        public async Task<bool> Exists<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity
        {
            var _dbSet = GetDbSet<TEntity>();
            return await _dbSet?.AnyAsync(query);
        }

        public async Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity
        {
            var _dbSet = GetDbSet<TEntity>();
            return await _dbSet?.CountAsync(query);
        }

        public async Task<Guid> InsertEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            Add(entity);
            await SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> UpdateEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            Update(entity);
            await SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            Remove(entity);
            await SaveChangesAsync();
            return true;
        }

        public async Task<int> InsertEntity<TEntity>(List<TEntity> entity) where TEntity : BaseEntity
        {
            AddRange(entity);
            await SaveChangesAsync();
            return entity.Count;
        }

        public async Task<int> UpdateEntity<TEntity>(List<TEntity> entity) where TEntity : BaseEntity
        {
            UpdateRange(entity);
            await SaveChangesAsync();
            return entity.Count;
        }

        public async Task<int> DeleteEntity<TEntity>(List<TEntity> entity) where TEntity : BaseEntity
        {
            RemoveRange(entity);
            await SaveChangesAsync();
            return entity.Count;
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var currentUserId = _sessionIdentifier.GetCurrentUser()?.Id ?? "";
            foreach (var entityEntry in ChangeTracker.Entries<BaseEntity>()) // Detects changes automatically
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Entity.CreatedById = currentUserId;
                    entityEntry.Entity.CreatedDate = DateTime.Now;
                }
                if (entityEntry.State == EntityState.Deleted)
                {
                    entityEntry.State = EntityState.Modified;
                    entityEntry.Entity.IsArchived = true;
                }
                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Entity.LastModifiedById = currentUserId;
                    entityEntry.Entity.LastModifiedDate = DateTime.Now;
                }
            }

            try
            {
                ChangeTracker.AutoDetectChangesEnabled = false;
                return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            finally
            {
                ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }
    }
}
