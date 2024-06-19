using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Models;
using Core.EntityModels.Base;
using Application.Models.Base;
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
        public async Task<ActionResult<ResponseModel<List<TDto>>>> GetAll()
        {
            var result = await _baseService.GetAll<TDto, TEntity>(x => !x.IsArchived);
            if (result == null)
            {
                return NotFound(ResponseModel<List<TDto>>.Failure());
            }
            return Ok(ResponseModel<List<TDto>>.Success(result));
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<ActionResult<ResponseModel<PaginatedResult<TDto>>>> GetAllPaginated(int start = 0, int recordsPerPage = int.MaxValue, string searchText = "")
        {
            var result = await _baseService.GetAllPaginated<TDto, TEntity>(x => !x.IsArchived, start, recordsPerPage, searchText);
            if (result == null)
            {
                return NotFound(ResponseModel<PaginatedResult<TDto>>.Failure());
            }
            return Ok(ResponseModel<PaginatedResult<TDto>>.Success(result));
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<ActionResult<ResponseModel<TDto>>> GetById(Guid id)
        {
            var result = await _baseService.GetOne<TDto, TEntity>(x => x.Id == id);
            if (result == null)
            {
                return NotFound(ResponseModel<TDto>.Failure());
            }
            return Ok(ResponseModel<TDto>.Success(result));
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<bool>), 200)]
        public async Task<IActionResult> Exists(Guid id)
        {
            var result = await _baseService.Exists<TEntity>(x => x.Id == id);
            return Ok(ResponseModel<bool>.Success(result));
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<int>), 200)]
        public async Task<IActionResult> Count(Guid id)
        {
            var result = await _baseService.Count<TEntity>(x => x.Id == id);
            return Ok(ResponseModel<int>.Success(result));
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<string>), 200)]
        public async Task<IActionResult> Create([FromBody] TCreateRequest entityRequest)
        {
            var result = await _baseService.InsertEntity<TCreateRequest, TEntity>(entityRequest);
            return Ok(ResponseModel<string>.Success(result));
        }

        [HttpPut]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<string>), 200)]
        public async Task<IActionResult> Update([FromBody] TUpdateRequest entityRequest)
        {
            var result = await _baseService.UpdateEntity<TUpdateRequest, TEntity>(entityRequest);
            return Ok(ResponseModel<string>.Success(result));
        }

        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<bool>), 200)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _baseService.DeleteEntity<TEntity>(id);
            return Ok(ResponseModel<bool>.Success(result));
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<int>), 200)]
        public async Task<IActionResult> CreateMany([FromBody] List<TCreateRequest> entityRequest)
        {
            var result = await _baseService.InsertEntity<TCreateRequest, TEntity>(entityRequest);
            return Ok(ResponseModel<int>.Success(result));
        }

        [HttpPut]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<int>), 200)]
        public async Task<IActionResult> UpdateMany([FromBody] List<TUpdateRequest> entityRequest)
        {
            var result = await _baseService.UpdateEntity<TUpdateRequest, TEntity>(entityRequest);
            return Ok(ResponseModel<int>.Success(result));
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<int>), 200)]
        public async Task<IActionResult> DeleteMany(List<Guid> id)
        {
            var result = await _baseService.DeleteEntity<TEntity>(id);
            return Ok(ResponseModel<int>.Success(result));
        }
    }
}
