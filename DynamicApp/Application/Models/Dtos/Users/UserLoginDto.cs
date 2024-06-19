using Application.Models.Base;

namespace Application.Models.Dtos.Users
{
    public class UserLoginDto : ILoginBase
    {
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
