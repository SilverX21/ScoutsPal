using Microsoft.EntityFrameworkCore;
using ScoutsPal.Services.EventsManagerAPI.Models;

namespace ScoutsPal.Services.EventsManagerAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<News> News { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ScoutEvents> ScoutEvents { get; set; }
    }
}
