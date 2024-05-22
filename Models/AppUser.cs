using Microsoft.AspNetCore.Identity;

namespace sbs.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname  { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
