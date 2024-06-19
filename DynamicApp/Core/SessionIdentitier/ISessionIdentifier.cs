using Core.Dtos;

namespace Core.SessionIdentitier
{
    public interface ISessionIdentifier
    {
        UserDto? GetCurrentUser();
        void GetUserDetails();
    }
}
