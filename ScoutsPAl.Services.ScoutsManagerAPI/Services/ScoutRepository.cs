using Microsoft.OpenApi.Writers;
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

        /// <summary>
        /// Method that creates a scout
        /// </summary>
        /// <param name="scout"></param>
        /// <returns></returns>
        public bool CreateScout(Scout scout)
        {
            if (scout == null)
            {
                return false;
            }

            try
            {
                if (ExistsScout(scout.ScoutId))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            _dbContext.Scouts.Add(scout);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// Method that deletes a given scout
        /// </summary>
        /// <param name="scoutId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteScout(Scout scout)
        {
            if (scout == null)
            {
                return false;
            }

            try
            {
                if (!ExistsScout(scout.ScoutId))
                {
                    return false;
                }

                _dbContext.Scouts.Remove(scout);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Method that updated a given scout info
        /// </summary>
        /// <param name="scout"></param>
        /// <returns></returns>
        public bool EditScout(Scout scout)
        {
            if (scout == null)
            {
                return false;
            }

            try
            {
                if (!ExistsScout(scout.ScoutId))
                {
                    return false;
                }

                _dbContext.Scouts.Update(scout);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a given scout exists
        /// </summary>
        /// <param name="scoutId"></param>
        /// <returns></returns>
        public bool ExistsScout(long scoutId)
        {
            if (scoutId <= 0)
            {
                return false;
            }

            return _dbContext.Scouts.Any(x => x.ScoutId == scoutId);
        }

        /// <summary>
        /// Gets a list of all scouts
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Scout> GetAllScout()
        {
            try
            {
                return _dbContext.Scouts.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a given scout details
        /// </summary>
        /// <param name="scoutId"></param>
        /// <returns></returns>
        public Scout GetScoutDetails(long scoutId)
        {
            if (scoutId <= 0)
            {
                return null;
            }

            try
            {
                if (!ExistsScout(scoutId))
                {
                    return null;
                }

                return _dbContext.Scouts.FirstOrDefault(x => x.ScoutId == scoutId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the scouts of a given group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IEnumerable<Scout> GetScoutsByGroup(int groupId)
        {
            List<Scout> scoutsList = new List<Scout>();

            if (groupId <= 0)
            {
                return scoutsList;
            }

            scoutsList = _dbContext.Scouts.Where(x => x.GroupId == groupId).ToList();

            return scoutsList;
        }

        /// <summary>
        /// Gets a list of scouts by their type
        /// </summary>
        /// <param name="scoutType"></param>
        /// <returns></returns>
        public IEnumerable<Scout> GetScoutsByType(int scoutType)
        {
            List<Scout> scoutsList = new List<Scout>();

            if (scoutType <= 0)
            {
                return scoutsList;
            }

            scoutsList = _dbContext.Scouts.Where(x => x.ScoutTypeId == scoutType).ToList();

            return scoutsList;
        }

        /// <summary>
        /// Checks if the given scout is active
        /// </summary>
        /// <param name="scoutId"></param>
        /// <returns></returns>
        public bool IsActiveScout(long scoutId)
        {
            if (scoutId <= 0)
            {
                return false;
            }

            var scout = _dbContext.Scouts.Find(scoutId);

            return scout != null ? scout.IsActive == true : false;
        }
    }
}