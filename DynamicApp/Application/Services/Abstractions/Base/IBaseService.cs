using Application.Models;
using Application.Models.Base;
using Core.EntityInterfaces.Base;
using Core.EntityModels.Base;
using System.Linq.Expressions;

namespace Application.Services.Abstractions.Base
{
    public interface IBaseService
    {
        Task<List<TDto>> GetAll<TDto, TEntity>(Expression<Func<TEntity, bool>> query, int start = 0, int recordsPerPage = int.MaxValue, string searchText = "") where TEntity : BaseEntity;
        Task<PaginatedResult<TDto>> GetAllPaginated<TDto, TEntity>(Expression<Func<TEntity, bool>> query, int start = 0, int recordsPerPage = int.MaxValue, string searchText = "") where TEntity : BaseEntity;
        Task<TDto?> GetOne<TDto, TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity;
        Task<bool> Exists<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity;
        Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity;
        Task<Guid> InsertEntity<TCreateRequest, TEntity>(TCreateRequest entityRequest) where TEntity : BaseEntity;
        Task<Guid> UpdateEntity<TUpdateRequest, TEntity>(TUpdateRequest entityRequest) where TEntity : BaseEntity where TUpdateRequest : IBaseId;
        Task<bool> DeleteEntity<TEntity>(Guid id) where TEntity : BaseEntity;
        Task<int> InsertEntity<TCreateRequest, TEntity>(List<TCreateRequest> entityRequest) where TEntity : BaseEntity;
        Task<int> UpdateEntity<TUpdateRequest, TEntity>(List<TUpdateRequest> entityRequest) where TEntity : BaseEntity where TUpdateRequest : IBaseId;
        Task<int> DeleteEntity<TEntity>(List<Guid> id) where TEntity : BaseEntity;
    }
}