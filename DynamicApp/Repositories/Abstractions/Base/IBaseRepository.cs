using Core.EntityModels.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories.Abstractions.Base
{
    public interface IBaseRepository
    {
        DbSet<TEntity>? GetDbSet<TEntity>() where TEntity : BaseEntity;
        Task<List<TEntity>> GetAll<TEntity>(Expression<Func<TEntity, bool>> query, int start = 0, int recordsPerPage = int.MaxValue, string searchText = "") where TEntity : BaseEntity;
        Task<TEntity?> GetOne<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity;
        Task<bool> Exists<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity;
        Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity;
        Task<Guid> InsertEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<Guid> UpdateEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<bool> DeleteEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<int> InsertEntity<TEntity>(List<TEntity> entity) where TEntity : BaseEntity;
        Task<int> UpdateEntity<TEntity>(List<TEntity> entity) where TEntity : BaseEntity;
        Task<int> DeleteEntity<TEntity>(List<TEntity> entity) where TEntity : BaseEntity;
    }
}
