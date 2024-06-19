using Core.Dtos;
using Core.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.SessionIdentitier
{
    public class SessionIdentifier : ISessionIdentifier
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public UserDto? CurrentUser;

        public SessionIdentifier(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void GetUserDetails()
        {
            try
            {
                var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"];
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadToken(accessToken) as JwtSecurityToken;
                if (token != null)
                {
                    CurrentUser = new UserDto(token.Claims);
                    if (string.IsNullOrEmpty(CurrentUser.Id))
                    {
                        CurrentUser = null;
                    }
                }
            }
            catch (Exception)
            {
                CurrentUser = null;
            }
        }

        public UserDto? GetCurrentUser()
        {
            if (CurrentUser == null) { GetUserDetails(); }
            return CurrentUser;
        }
    }
}
