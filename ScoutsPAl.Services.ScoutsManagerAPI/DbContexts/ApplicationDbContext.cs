using Microsoft.EntityFrameworkCore;
using ScoutsPAl.Services.ScoutsManagerAPI.Models;

namespace ScoutsPAl.Services.ScoutsManagerAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Scout> Scouts { get; set; }
    }
}