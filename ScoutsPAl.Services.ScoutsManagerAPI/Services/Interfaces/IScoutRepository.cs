using Microsoft.AspNetCore.Mvc;
using ScoutsPAl.Services.ScoutsManagerAPI.Models;

namespace ScoutsPAl.Services.ScoutsManagerAPI.Services.Interfaces
{
    public interface IScoutRepository
    {
        public IEnumerable<Scout> GetScoutsByType(int scoutType);

        public Task<IEnumerable<Scout>> GetAllScout();

        public Task<IEnumerable<Scout>> GetScoutsByGroup(int groupId);

        public Task<Scout> GetScoutDetails(int scoutType);

        public Task<bool> CreateScout(Scout scout);

        public Task<bool> EditScout(Scout scout);

        public Task<bool> DeleteScout(int scoutId);

        public Task<bool> IsActiveScout(int scoutId);
    }
}