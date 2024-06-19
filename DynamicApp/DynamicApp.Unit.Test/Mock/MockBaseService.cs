using Application.Services.Abstractions.Base;
using Core.EntityModels;
using Moq;
using System.Linq.Expressions;
using Core.EntityModels.Base;
using Application.Models;
using Application.Models.Base;



namespace DynamicApp.Unit.Test.Mock
{
    public class MockBaseService : IBaseService
    {
        private readonly Mock<IBaseService> mock;

        public MockBaseService()
        {
            mock = new Mock<IBaseService>();
        }

        public Mock<IBaseService> Mock => mock;

        public Task<List<TDto>> GetAll<TDto, TEntity>(Expression<Func<TEntity, bool>> query, int start = 0, int recordsPerPage = int.MaxValue, string searchText = "") where TEntity : BaseEntity
        {
            return mock.Object.GetAll<TDto, TEntity>(query, start, recordsPerPage, searchText);
        }

        public Task<PaginatedResult<TDto>> GetAllPaginated<TDto, TEntity>(Expression<Func<TEntity, bool>> query, int start = 0, int recordsPerPage = int.MaxValue, string searchText = "") where TEntity : BaseEntity
        {
            return mock.Object.GetAllPaginated<TDto, TEntity>(query, start, recordsPerPage, searchText);
        }

        public Task<TDto?> GetOne<TDto, TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity
        {
            return mock.Object.GetOne<TDto, TEntity>(query);
        }

        public Task<bool> Exists<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity
        {
            if (typeof(TEntity) == typeof(ProgramForm))
            {
                var programForm = new ProgramForm 
                { 
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
                };
                return Task.FromResult(programForm.Id == Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"));
            }

            return mock.Object.Exists<TEntity>(query);
        }

        public Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> query) where TEntity : BaseEntity
        {
            return mock.Object.Count<TEntity>(query);
        }

        public Task<Guid> InsertEntity<TCreateRequest, TEntity>(TCreateRequest entityRequest) where TEntity : BaseEntity
        {
            return mock.Object.InsertEntity<TCreateRequest, TEntity>(entityRequest);
        }

        public Task<Guid> UpdateEntity<TUpdateRequest, TEntity>(TUpdateRequest entityRequest) where TEntity : BaseEntity where TUpdateRequest : IBaseId
        {
            return mock.Object.UpdateEntity<TUpdateRequest, TEntity>(entityRequest);
        }

        public Task<bool> DeleteEntity<TEntity>(Guid id) where TEntity : BaseEntity
        {
            return mock.Object.DeleteEntity<TEntity>(id);
        }

        public Task<int> InsertEntity<TCreateRequest, TEntity>(List<TCreateRequest> entityRequest) where TEntity : BaseEntity
        {
            return mock.Object.InsertEntity<TCreateRequest, TEntity>(entityRequest);
        }

        public Task<int> UpdateEntity<TUpdateRequest, TEntity>(List<TUpdateRequest> entityRequest) where TEntity : BaseEntity where TUpdateRequest : IBaseId
        {
            return mock.Object.UpdateEntity<TUpdateRequest, TEntity>(entityRequest);
        }

        public Task<int> DeleteEntity<TEntity>(List<Guid> id) where TEntity : BaseEntity
        {
            return mock.Object.DeleteEntity<TEntity>(id);
        }
    }

}
