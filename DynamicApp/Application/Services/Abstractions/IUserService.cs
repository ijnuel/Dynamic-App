using Application.Models.Dtos.Users;
using Microsoft.IdentityModel.Tokens;

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
