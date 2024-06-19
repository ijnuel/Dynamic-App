using Core.EntityModels;
using Core.EntityModels.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DbContext
{
    public interface IApplicationDBContext
    {

        DbSet<FormResponses> FormResponses { get; set; }
        DbSet<ProgramForm> ProgramForms { get; set; }

        Task<TEntity?> GetOne<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity;
        Task<bool> Exists<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity;
        Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity;
        Task<List<TEntity>> GetMany<TEntity>(Expression<Func<TEntity, bool>> query, int start = 0, int recordsPerPage = int.MaxValue, string searchText = "") where TEntity : BaseEntity;
        Task<Guid> InsertEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<int> InsertEntity<TEntity>(List<TEntity> entity) where TEntity : BaseEntity;
        Task<Guid> UpdateEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<int> UpdateEntity<TEntity>(List<TEntity> entity) where TEntity : BaseEntity;
        Task<bool> DeleteEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<int> DeleteEntity<TEntity>(List<TEntity> entity) where TEntity : BaseEntity;
        DbSet<TEntity>? GetDbSet<TEntity>() where TEntity : BaseEntity;
    }
}
