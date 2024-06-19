using Application.Models;
using Application.Models.Base;
using Application.Services.Abstractions.Base;
using AutoMapper;
using Core.EntityInterfaces.Base;
using Core.EntityModels.Base;
using Repositories.Abstractions.Base;
using System.Linq.Expressions;

namespace Application.Services.Implementations.Base
{
    public class BaseService : IBaseService
    {
        private readonly IBaseRepository _baseRepo;
        private readonly IMapper _mapper;
        public BaseService(IBaseRepository baseRepo, IMapper mapper)
        {
            _baseRepo = baseRepo;
            _mapper = mapper;
        }


        public async Task<List<TDto>> GetAll<TDto, TEntity>(Expression<Func<TEntity, bool>> query, int start = 0, int recordsPerPage = int.MaxValue, string searchText = "") where TEntity : BaseEntity
        {
            var result = await _baseRepo.GetAll(query, start, recordsPerPage, searchText);
            return _mapper.Map<List<TDto>>(result);
        }
        public async Task<PaginatedResult<TDto>> GetAllPaginated<TDto, TEntity>(Expression<Func<TEntity, bool>> query, int start = 0, int recordsPerPage = int.MaxValue, string searchText = "") where TEntity : BaseEntity
        {
            var resultData = await GetAll<TDto, TEntity>(query, start, recordsPerPage, searchText);
            var resultCount = (await Count(query));
            var result = new PaginatedResult<TDto>(start, recordsPerPage, resultCount, resultData);
            return result;
        }

        public async Task<TDto?> GetOne<TDto, TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity
        {
            var result = await _baseRepo.GetOne(query);
            return _mapper.Map<TDto>(result);
        }

        public async Task<bool> Exists<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity
        {
            return await _baseRepo.Exists(query);
        }

        public async Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity
        {
            return await _baseRepo.Count(query);
        }

        public async Task<Guid> InsertEntity<TCreateRequest, TEntity>(TCreateRequest entityRequest) where TEntity : BaseEntity
        {
            var entity = _mapper.Map<TEntity>(entityRequest);
            return await _baseRepo.InsertEntity(entity);
        }

        public async Task<Guid> UpdateEntity<TUpdateRequest, TEntity>(TUpdateRequest entityRequest) where TEntity : BaseEntity where TUpdateRequest : IBaseId
        {
            var existingEntity = await _baseRepo.GetOne<TEntity>(x => x.Id == entityRequest.Id);
            if (existingEntity != null)
            {
                var entity = _mapper.Map(entityRequest, existingEntity);
                return await _baseRepo.UpdateEntity(entity);
            }
            return Guid.Empty;
        }

        public async Task<bool> DeleteEntity<TEntity>(Guid id) where TEntity : BaseEntity
        {
            var entity = await _baseRepo.GetOne<TEntity>(x => x.Id == id);
            return entity != null ? await _baseRepo.DeleteEntity(entity) : false;
        }

        public async Task<int> InsertEntity<TCreateRequest, TEntity>(List<TCreateRequest> entityRequest) where TEntity : BaseEntity
        {
            var entity = _mapper.Map<List<TEntity>>(entityRequest);
            return await _baseRepo.InsertEntity(entity);
        }

        public async Task<int> UpdateEntity<TUpdateRequest, TEntity>(List<TUpdateRequest> entityRequest) where TEntity : BaseEntity where TUpdateRequest : IBaseId
        {
            var updateIds = entityRequest.Select(x => x.Id).ToList();
            var existingEntity = await _baseRepo.GetAll<TEntity>(x => updateIds.Contains(x.Id));
            if (existingEntity != null)
            {
                var entity = _mapper.Map(entityRequest, existingEntity);
                return await _baseRepo.UpdateEntity(entity);
            }
            return 0;
        }

        public async Task<int> DeleteEntity<TEntity>(List<Guid> id) where TEntity : BaseEntity
        {
            var entity = await _baseRepo.GetAll<TEntity>(x => id.Contains(x.Id));
            return entity != null ? await _baseRepo.DeleteEntity(entity) : 0;
        }
    }
}
