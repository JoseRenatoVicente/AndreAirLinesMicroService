using AndreAirLines.Domain.Entities.Base;

namespace AndreAirLines.Domain.Entities
{
    public class User : Person
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Sector { get; set; }
        public Role Role { get; set; }
    }
}
