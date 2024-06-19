using API.Controllers.Base;
using Application.Models;
using Application.Models.Dtos;
using Application.Services.Abstractions;
using Core.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [AllowAnonymous]
    public class FormResponsesController : BaseEntityController<FormResponsesResponseDto, FormResponses, FormResponsesCreateDto, FormResponsesUpdateDto>
    {
        private readonly IFormResponsesService _formResponsesService;
        public FormResponsesController(IFormResponsesService formResponsesService) : base(formResponsesService)
        {
            _formResponsesService = formResponsesService;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<ActionResult<ResponseModel<List<FormResponsesResponseDto>>>> GetResponsesByProgramFormId(Guid id)
        {
            var result = await _formResponsesService.GetAll<FormResponsesResponseDto, FormResponses>(x => x.ProgramFormId == id);
            if (result == null)
            {
                return NotFound(ResponseModel<List<FormResponsesResponseDto>>.Failure());
            }
            return Ok(ResponseModel<List<FormResponsesResponseDto>>.Success(result));
        }
    }
}
