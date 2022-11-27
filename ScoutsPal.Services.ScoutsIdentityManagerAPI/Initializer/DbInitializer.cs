using IdentityModel;
using Microsoft.AspNetCore.Identity;
using ScoutsPal.Services.ScoutsIdentityManagerAPI.DbContexts;
using ScoutsPal.Services.ScoutsIdentityManagerAPI.Models;
using System.Security.Claims;

namespace ScoutsPal.Services.ScoutsIdentityManagerAPI.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(StaticDetails.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Standard)).GetAwaiter().GetResult();
            }
            else
            {
                return;
            }

            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "Pedro.Admin",
                Email = "pedro.admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "111111111",
                FirstName = "Pedro",
                LastName = "Gomes"
            };

            _userManager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser, StaticDetails.Admin).GetAwaiter().GetResult();

            var temp1 = _userManager.AddClaimsAsync(adminUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, adminUser.FirstName+" "+ adminUser.LastName),
                new Claim(JwtClaimTypes.Role, StaticDetails.Admin),
            }).Result;

            ApplicationUser standardUser = new ApplicationUser()
            {
                UserName = "Pedro.Standard",
                Email = "pedro.standard@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "111111111",
                FirstName = "Pedro",
                LastName = "Gomes"
            };

            _userManager.CreateAsync(standardUser, "Standard123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(standardUser, StaticDetails.Standard).GetAwaiter().GetResult();

            var temp2 = _userManager.AddClaimsAsync(standardUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, standardUser.FirstName+" "+ standardUser.LastName),
                new Claim(JwtClaimTypes.Role, StaticDetails.Standard),
            }).Result;
        }
    }
}
