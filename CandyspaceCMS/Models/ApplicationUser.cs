using Microsoft.AspNetCore.Identity;

namespace CandyspaceCMS.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; }
    }
}
