using Core.EntityInterfaces;
using Microsoft.AspNetCore.Identity;

namespace Core.EntityModels
{
    public class AppUser : IdentityUser, IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
