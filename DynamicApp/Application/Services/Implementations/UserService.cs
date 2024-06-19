using Application.Models;
using Application.Models.Dtos;
using Application.Models.Dtos.Users;
using Application.Services.Abstractions;
using Application.Services.Abstractions.Base;
using AutoMapper;
using Azure;
using Azure.Core;
using Core.Dtos;
using Core.EntityModels;
using Core.Enums;
using Core.SessionIdentitier;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JwtConfig _jwtConfig;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISessionIdentifier _sessionIdentifier;
        private readonly IBaseService _baseService;

        public UserService(SignInManager<AppUser> signInManager,
            IOptions<JwtConfig> options,
            UserManager<AppUser> userManager, ILogger<UserService> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ISessionIdentifier sessionIdentifier,
            IBaseService baseService
            )
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _jwtConfig = options.Value;
            _httpContextAccessor = httpContextAccessor;
            _sessionIdentifier = sessionIdentifier;
            _baseService = baseService;
        }
        public async Task<(bool, UserResponseDto)> CreateUser(CreateUserDto userDto)
        {
            AppUser user = _mapper.Map<AppUser>(userDto);
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                user.UserName = user.Email;
            }
            if (await _userManager.FindByEmailAsync(user?.Email ?? "") == null)
            {
                var result = await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    await _userManager.ConfirmEmailAsync(user, code);
                    return (true, new UserResponseDto("User created successfully!", user.Id));
                }
                return (false, new UserResponseDto(result.Errors.Select(x => x.Description).ToList()));
            }
            return (false, new UserResponseDto("Email already in use!"));
        }
        public async Task<(bool, UserResponseDto)> Login(UserLoginDto userLoginDto)
        {
            AppUser user = (await _userManager.FindByNameAsync(userLoginDto.UserName)) ?? (await _userManager.FindByEmailAsync(userLoginDto.Email));
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    var issuer = _jwtConfig.Issuer;
                    var audience = _jwtConfig.Audience;
                    var key = Encoding.ASCII.GetBytes(_jwtConfig.Key);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                                new Claim("id", user.Id),
                                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                                new Claim("firstName", user.FirstName),
                                new Claim("lasrtName", user.LastName),
                                new Claim(JwtRegisteredClaimNames.Jti,
                                Guid.NewGuid().ToString())
                            }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
                    };
                    string token = SetTokenCookie(tokenDescriptor, "AccessToken");
                    var userResponse = new UserResponseDto("Successful Login", token);
                    userResponse.CurrentUser = new UserDto(user);
                    return (true, userResponse);
                }
            }
            return (false, new UserResponseDto("Incorrect Credentials!"));
        }

        public async Task<(bool, UserResponseDto)> Logout()
        {
            if (_sessionIdentifier.GetCurrentUser() == null)
            {
                return (false, new UserResponseDto("No users logged in!"));
            }
            //_httpContextAccessor.HttpContext?.Response.Cookies.Delete("AccessToken");
            string token = SetTokenCookie(new SecurityTokenDescriptor(), "AccessToken", true);
            var userResponse = new UserResponseDto("Successful Logout", token);
            userResponse.CurrentUser = null;
            return (true, userResponse);
        }


        public string SetTokenCookie(SecurityTokenDescriptor tokenDescriptor, string tokenType, bool delete = false)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            var cookieOptions = new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                Expires = delete ? DateTime.UtcNow.AddDays(-1) : token?.ValidTo ?? DateTime.UtcNow.AddHours(1),
                SameSite = SameSiteMode.None
            };
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(tokenType, stringToken, cookieOptions);
            return stringToken;
        }
    }
}
