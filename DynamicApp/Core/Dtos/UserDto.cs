using Core.EntityInterfaces;
using Core.EntityModels;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class UserDto
    {
        public UserDto(IEnumerable<Claim> claims)
        {
            Id = claims.FirstOrDefault(x => x.Type == "id")?.Value ?? "";
        }

        public UserDto(AppUser user)
        {
            Id = user.Id;
        }

        public string Id { get; set; }
    }
}
