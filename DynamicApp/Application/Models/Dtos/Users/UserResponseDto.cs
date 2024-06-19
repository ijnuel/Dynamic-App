using Core.Dtos;

namespace Application.Models.Dtos.Users
{
    public class UserResponseDto
    {
        public UserResponseDto(string message, string token = "")
        {
            Message = new List<string> { message };
            Token = token;
        }
        public UserResponseDto(List<string> messages, string token = "")
        {
            Message = new List<string>(messages);
            Token = token;
        }
        public List<string> Message { get; set; }
        public string Token { get; set; }
        public UserDto? CurrentUser { get; set; }

    }
}
