using Microsoft.AspNetCore.Mvc;
using ScoutsPAl.Services.ScoutsManagerAPI.Models;

namespace ScoutsPAl.Services.ScoutsManagerAPI.Services.Interfaces
{
    public interface IScoutTypeRepository
    {
        public IEnumerable<ScoutType> GetScoutTypes();

        public ScoutType GetScoutTypeDetails(int scoutTypeId);

        public bool CreateScoutType(ScoutType scoutType);

        public bool EditScoutType(ScoutType scoutType);

        public bool DeleteScoutType(ScoutType scoutType);

        public bool ExistsScoutType(int scoutTypeId);
    }
}