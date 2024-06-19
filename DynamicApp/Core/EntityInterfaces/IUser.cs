using Core.Enums;

namespace Core.EntityInterfaces
{
    public interface IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
