using Core.DbContext;
using Core.EntityModels.Base;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions.Base;
using System.Linq.Expressions;

namespace Repositories.Implementations.Base
{
    public class BaseRepository : IBaseRepository
    {
        private readonly IApplicationDBContext _dbContext;

        public BaseRepository(IApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<TEntity>? GetDbSet<TEntity>() where TEntity : BaseEntity
        {
            return _dbContext.GetDbSet<TEntity>();
        }

        public async Task<List<TEntity>> GetAll<TEntity>(Expression<Func<TEntity, bool>> query, int start = 0, int recordsPerPage = int.MaxValue, string searchText = "") where TEntity : BaseEntity
        {

            var result = await _dbContext.GetMany(query, start, recordsPerPage, searchText);

            return result;
        }

        public async Task<TEntity?> GetOne<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity
        {
            var result = await _dbContext.GetOne(query);

            return result;
        }

        public async Task<bool> Exists<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity
        {
            var result = await _dbContext.Exists(query);

            return result;
        }

        public async Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity
        {
            var result = await _dbContext.Count(query);

            return result;
        }

        public async Task<bool> DeleteEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var result = await _dbContext.DeleteEntity(entity);

            return result;
        }

        public async Task<Guid> InsertEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var result = await _dbContext.InsertEntity(entity);

            return result;
        }

        public async Task<Guid> UpdateEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var result = await _dbContext.UpdateEntity(entity);

            return result;
        }

        public async Task<int> DeleteEntity<TEntity>(List<TEntity> entity) where TEntity : BaseEntity
        {
            var result = await _dbContext.DeleteEntity(entity);

            return result;
        }

        public async Task<int> InsertEntity<TEntity>(List<TEntity> entity) where TEntity : BaseEntity
        {
            var result = await _dbContext.InsertEntity(entity);

            return result;
        }

        public async Task<int> UpdateEntity<TEntity>(List<TEntity> entity) where TEntity : BaseEntity
        {
            var result = await _dbContext.UpdateEntity(entity);

            return result;
        }
    }
}
