using Microsoft.AspNetCore.Identity;

namespace ScoutsPal.Services.ScoutsIdentityManagerAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
