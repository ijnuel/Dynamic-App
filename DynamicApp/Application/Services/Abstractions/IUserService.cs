using Application.Models.Dtos;
using Application.Models.Dtos.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Abstractions
{
    public interface IUserService
    {
        Task<(bool, UserResponseDto)> CreateUser(CreateUserDto userDto);
        Task<(bool, UserResponseDto)> Login(UserLoginDto userLoginDto);
        Task<(bool, UserResponseDto)> Logout();
        string SetTokenCookie(SecurityTokenDescriptor tokenDescriptor, string tokenType, bool delete = false);
    }
}
