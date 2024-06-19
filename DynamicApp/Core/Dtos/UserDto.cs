using Core.EntityModels;
using System.Security.Claims;

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
