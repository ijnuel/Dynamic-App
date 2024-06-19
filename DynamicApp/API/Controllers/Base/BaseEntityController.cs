using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Models;
using Core.EntityInterfaces.Base;
using Core.EntityModels.Base;
using Application.Models.Base;
using Microsoft.AspNetCore.Authorization;
using Application.Services.Abstractions.Base;

namespace API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseEntityController<TDto, TEntity, TCreateRequest, TUpdateRequest> : ControllerBase where TEntity : BaseEntity where TUpdateRequest : IBaseId
    {
        private readonly IBaseService _baseService;

        public BaseEntityController(IBaseService baseService)
        {
            _baseService = baseService;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<ActionResult<Result<List<TDto>>>> GetAll()
        {
            var result = await _baseService.GetAll<TDto, TEntity>(x => !x.IsArchived);
            if (result == null)
            {
                return NotFound(Result<List<TDto>>.Failure());
            }
            return Ok(Result<List<TDto>>.Success(result));
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<ActionResult<Result<PaginatedResult<TDto>>>> GetAllPaginated(int start = 0, int recordsPerPage = int.MaxValue, string searchText = "")
        {
            var result = await _baseService.GetAllPaginated<TDto, TEntity>(x => !x.IsArchived, start, recordsPerPage, searchText);
            if (result == null)
            {
                return NotFound(Result<PaginatedResult<TDto>>.Failure());
            }
            return Ok(Result<PaginatedResult<TDto>>.Success(result));
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<ActionResult<Result<TDto>>> GetById(Guid id)
        {
            var result = await _baseService.GetOne<TDto, TEntity>(x => x.Id == id);
            if (result == null)
            {
                return NotFound(Result<TDto>.Failure());
            }
            return Ok(Result<TDto>.Success(result));
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(Result<bool>), 200)]
        public async Task<IActionResult> Exists(Guid id)
        {
            var result = await _baseService.Exists<TEntity>(x => x.Id == id);
            return Ok(Result<bool>.Success(result));
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(Result<int>), 200)]
        public async Task<IActionResult> Count(Guid id)
        {
            var result = await _baseService.Count<TEntity>(x => x.Id == id);
            return Ok(Result<int>.Success(result));
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(Result<string>), 200)]
        public async Task<IActionResult> Create([FromBody] TCreateRequest entityRequest)
        {
            var result = await _baseService.InsertEntity<TCreateRequest, TEntity>(entityRequest);
            return Ok(Result<string>.Success(result));
        }

        [HttpPut]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(Result<string>), 200)]
        public async Task<IActionResult> Update([FromBody] TUpdateRequest entityRequest)
        {
            var result = await _baseService.UpdateEntity<TUpdateRequest, TEntity>(entityRequest);
            return Ok(Result<string>.Success(result));
        }

        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(Result<bool>), 200)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _baseService.DeleteEntity<TEntity>(id);
            return Ok(Result<bool>.Success(result));
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(Result<int>), 200)]
        public async Task<IActionResult> CreateMany([FromBody] List<TCreateRequest> entityRequest)
        {
            var result = await _baseService.InsertEntity<TCreateRequest, TEntity>(entityRequest);
            return Ok(Result<int>.Success(result));
        }

        [HttpPut]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(Result<int>), 200)]
        public async Task<IActionResult> UpdateMany([FromBody] List<TUpdateRequest> entityRequest)
        {
            var result = await _baseService.UpdateEntity<TUpdateRequest, TEntity>(entityRequest);
            return Ok(Result<int>.Success(result));
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(Result<int>), 200)]
        public async Task<IActionResult> DeleteMany(List<Guid> id)
        {
            var result = await _baseService.DeleteEntity<TEntity>(id);
            return Ok(Result<int>.Success(result));
        }
    }
}
