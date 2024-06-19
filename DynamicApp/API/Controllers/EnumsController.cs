using Application.Helpers;
using Application.Models;
using Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumsController : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(Result<List<EnumResponseModel>>), 200)]
        public IActionResult GetQuestionType()
        {
            var result = EnumUtil.GetEnum<QuestionType>();
            return Ok(Result<List<EnumResponseModel>>.Success(result));
        }
    }
}
