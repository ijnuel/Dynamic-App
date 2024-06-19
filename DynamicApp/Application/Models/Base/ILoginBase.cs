namespace Application.Models.Base
{
    public interface ILoginBase
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
