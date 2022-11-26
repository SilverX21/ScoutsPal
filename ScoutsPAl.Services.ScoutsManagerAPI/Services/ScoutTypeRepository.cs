using Microsoft.OpenApi.Writers;
using ScoutsPAl.Services.ScoutsManagerAPI.DbContexts;
using ScoutsPAl.Services.ScoutsManagerAPI.Models;
using ScoutsPAl.Services.ScoutsManagerAPI.Services.Interfaces;

namespace ScoutsPAl.Services.ScoutsManagerAPI.Services
{
    public class ScoutTypeRepository : IScoutTypeRepository
    {
        private readonly ILogger<ScoutTypeRepository> _logger;
        private readonly ApplicationDbContext _dbContext;

        public ScoutTypeRepository(ILogger<ScoutTypeRepository> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method that creates a scout type
        /// </summary>
        /// <param name="scoutType"></param>
        /// <returns></returns>
        public bool CreateScoutType(ScoutType scoutType)
        {
            if (scoutType == null)
            {
                return false;
            }

            try
            {
                if (ExistsScoutType(scoutType.ScoutTypeId))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            _dbContext.ScoutTypes.Add(scoutType);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// Method that deletes a given scout type
        /// </summary>
        /// <param name="scoutType"></param>
        /// <returns></returns>
        public bool DeleteScoutType(ScoutType scoutType)
        {
            if (scoutType == null)
            {
                return false;
            }

            try
            {
                if (!ExistsScoutType(scoutType.ScoutTypeId))
                {
                    return false;
                }

                _dbContext.ScoutTypes.Remove(scoutType);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Method that updates a given scout type info
        /// </summary>
        /// <param name="scoutType"></param>
        /// <returns></returns>
        public bool EditScoutType(ScoutType scoutType)
        {
            if (scoutType == null)
            {
                return false;
            }

            try
            {
                if (!ExistsScoutType(scoutType.ScoutTypeId))
                {
                    return false;
                }

                _dbContext.ScoutTypes.Update(scoutType);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Method that checks if a given scout type exists
        /// </summary>
        /// <param name="scoutTypeId"></param>
        /// <returns></returns>
        public bool ExistsScoutType(int scoutTypeId)
        {
            if (scoutTypeId <= 0)
            {
                return false;
            }

            return _dbContext.ScoutTypes.Any(x => x.ScoutTypeId == scoutTypeId);
        }

        /// <summary>
        /// Gets a list of all scout types
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ScoutType> GetScoutTypes()
        {
            try
            {
                return _dbContext.ScoutTypes.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// gets the details of a given scout type
        /// </summary>
        /// <param name="scoutTypeId"></param>
        /// <returns></returns>
        public ScoutType GetScoutTypeDetails(int scoutTypeId)
        {
            if (scoutTypeId <= 0)
            {
                return null;
            }

            try
            {
                if (!ExistsScoutType(scoutTypeId))
                {
                    return null;
                }

                return _dbContext.ScoutTypes.FirstOrDefault(x => x.ScoutTypeId == scoutTypeId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}