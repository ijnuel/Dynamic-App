using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using System.Net;

using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http.HttpResults;
using Application.Services.Abstractions;
using Application.Models.Dtos;
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
        [ProducesResponseType(typeof(Result<UserResponseDto>), 200)]
        public async Task<IActionResult> CreateAccount(CreateUserDto userDto)
        {
            var (successfulCreate, result) = await _userService.CreateUser(userDto);
            if (successfulCreate)
            {
                return Ok(Result<UserResponseDto>.Success(result.Message.FirstOrDefault() ?? "", result));
            }
            return BadRequest(Result<UserResponseDto>.Failure(result.Message.FirstOrDefault() ?? "", result));
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<UserResponseDto>), 200)]
        [ProducesResponseType(typeof(Result<UserResponseDto>), 401)]
        [ProducesResponseType(typeof(Result<UserResponseDto>), 403)]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var (successfulLogin, result) = await _userService.Login(userLoginDto);
            if (successfulLogin)
            {
                return Ok(Result<UserResponseDto>.Success(result.Message.FirstOrDefault() ?? "", result));
            }
            return BadRequest(Result<UserResponseDto>.Failure(result.Message.FirstOrDefault() ?? "", result));
        }


        [Authorize]
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<UserResponseDto>), 200)]
        [ProducesResponseType(typeof(Result<UserResponseDto>), 401)]
        [ProducesResponseType(typeof(Result<UserResponseDto>), 403)]
        public async Task<IActionResult> Logout()
        {
            var (successfulLogin, result) = await _userService.Logout();
            if (successfulLogin)
            {
                return Ok(Result<UserResponseDto>.Success(result.Message.FirstOrDefault() ?? "", result));
            }
            return BadRequest(Result<UserResponseDto>.Failure(result.Message.FirstOrDefault() ?? "", result));
        }
    }
}
