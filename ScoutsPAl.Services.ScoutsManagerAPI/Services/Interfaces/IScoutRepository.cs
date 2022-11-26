using Microsoft.AspNetCore.Mvc;
using ScoutsPAl.Services.ScoutsManagerAPI.Models;

namespace ScoutsPAl.Services.ScoutsManagerAPI.Services.Interfaces
{
    public interface IScoutRepository
    {
        public IEnumerable<Scout> GetScoutsByType(int scoutType);

        public IEnumerable<Scout> GetAllScout();

        public IEnumerable<Scout> GetScoutsByGroup(int groupId);

        public Scout GetScoutDetails(int scoutId);

        public bool CreateScout(Scout scout);

        public bool EditScout(Scout scout);

        public bool DeleteScout(Scout scout);

        public bool IsActiveScout(long scoutId);

        public bool ExistsScout(long scoutId);
    }
}