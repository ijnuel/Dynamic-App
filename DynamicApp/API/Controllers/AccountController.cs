using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Services.Abstractions;
using Application.Models.Dtos.Users;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(
            IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDto>), 200)]
        public async Task<IActionResult> CreateAccount(CreateUserDto userDto)
        {
            var (successfulCreate, result) = await _userService.CreateUser(userDto);
            if (successfulCreate)
            {
                return Ok(ResponseModel<UserResponseDto>.Success(result.Message.FirstOrDefault() ?? "", result));
            }
            return BadRequest(ResponseModel<UserResponseDto>.Failure(result.Message.FirstOrDefault() ?? "", result));
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDto>), 401)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDto>), 403)]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var (successfulLogin, result) = await _userService.Login(userLoginDto);
            if (successfulLogin)
            {
                return Ok(ResponseModel<UserResponseDto>.Success(result.Message.FirstOrDefault() ?? "", result));
            }
            return BadRequest(ResponseModel<UserResponseDto>.Failure(result.Message.FirstOrDefault() ?? "", result));
        }


        [Authorize]
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDto>), 401)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDto>), 403)]
        public async Task<IActionResult> Logout()
        {
            var (successfulLogin, result) = await _userService.Logout();
            if (successfulLogin)
            {
                return Ok(ResponseModel<UserResponseDto>.Success(result.Message.FirstOrDefault() ?? "", result));
            }
            return BadRequest(ResponseModel<UserResponseDto>.Failure(result.Message.FirstOrDefault() ?? "", result));
        }
    }
}
