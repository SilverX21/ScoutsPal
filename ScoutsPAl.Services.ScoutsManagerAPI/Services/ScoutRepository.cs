using ScoutsPAl.Services.ScoutsManagerAPI.DbContexts;
using ScoutsPAl.Services.ScoutsManagerAPI.Models;
using ScoutsPAl.Services.ScoutsManagerAPI.Services.Interfaces;

namespace ScoutsPAl.Services.ScoutsManagerAPI.Services
{
    public class ScoutRepository : IScoutRepository
    {
        private readonly ILogger<ScoutRepository> _logger;
        private readonly ApplicationDbContext _dbContext;

        public ScoutRepository(ILogger<ScoutRepository> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public Task<bool> CreateScout(Scout scout)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteScout(int scoutId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditScout(Scout scout)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Scout>> GetAllScout()
        {
            throw new NotImplementedException();
        }

        public Task<Scout> GetScoutDetails(int scoutType)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Scout>> GetScoutsByGroup(int groupId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Scout> GetScoutsByType(int scoutType)
        {
            List<Scout> scoutsList = new List<Scout>();

            scoutsList.Add(new Scout { Name = "Menaço", AdmissionDate = DateTime.Now });
            return scoutsList;
        }

        public Task<bool> IsActiveScout(int scoutId)
        {
            throw new NotImplementedException();
        }
    }
}