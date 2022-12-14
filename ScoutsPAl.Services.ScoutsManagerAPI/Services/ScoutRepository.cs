using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Writers;
using ScoutsPAl.Services.ScoutsManagerAPI.DbContexts;
using ScoutsPAl.Services.ScoutsManagerAPI.Models;
using ScoutsPAl.Services.ScoutsManagerAPI.Services.Interfaces;
using Serilog;

namespace ScoutsPAl.Services.ScoutsManagerAPI.Services
{
    public class ScoutRepository : IScoutRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Serilog.ILogger _serilogLogger;

        public ScoutRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _serilogLogger = Log.ForContext<ScoutRepository>();
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
                _serilogLogger.Warning("ScoutRepository: The inputed data is null");
                return false;
            }

            try
            {
                if (ExistsScout(scout.ScoutId))
                {
                    _serilogLogger.Warning("ScoutRepository: Scout doesn't exist!");
                    return false;
                }

                _dbContext.Scouts.Add(scout);
                _dbContext.SaveChanges();
                _serilogLogger.Information("ScoutRepository: The scout was created!");
                return true;
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "ScoutRepository: There was a problem during the execution");
                return false;
            }
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
                _serilogLogger.Warning("ScoutRepository: The inputed data is null");
                return false;
            }

            try
            {
                if (!ExistsScout(scout.ScoutId))
                {
                    _serilogLogger.Warning("ScoutRepository: The scout doesn't exist!");
                    return false;
                }

                _dbContext.Scouts.Remove(scout);
                _dbContext.SaveChanges();
                _serilogLogger.Information("ScoutRepository: The scout was removed!");
                return true;
            }
            catch (Exception ex)
            {
                _serilogLogger.Warning(ex.Message, "ScoutRepository: There was a problem during the execution");
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
                _serilogLogger.Warning("ScoutRepository: The inputed data is null");
                return false;
            }

            try
            {
                if (!ExistsScout(scout.ScoutId))
                {
                    _serilogLogger.Warning("ScoutRepository: The scout doesn't exist!");
                    return false;
                }

                _dbContext.Scouts.Update(scout);
                _dbContext.SaveChanges();
                _serilogLogger.Information("ScoutRepository: The scout info was updated!");
                return true;
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "ScoutRepository: There was a problem during the execution");
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
                _serilogLogger.Warning("ScoutRepository: The inputed data is not valid");
                return false;
            }

            _serilogLogger.Information("ScoutRepository: confirmed scout existence!");
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
                _serilogLogger.Error(ex.Message, "ScoutRepository: There was a problem during the execution!");
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
                _serilogLogger.Warning("ScoutRepository: The inputed data isn't valid!");
                return null;
            }

            try
            {
                if (!ExistsScout(scoutId))
                {
                    _serilogLogger.Warning("ScoutRepository: The scout doesn't exist!");
                    return null;
                }

                _serilogLogger.Information("ScoutRepository: Fetched the scout info!");
                return _dbContext.Scouts.FirstOrDefault(x => x.ScoutId == scoutId);
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "ScoutRepository: There was a problem during the execution!");
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
                _serilogLogger.Warning("ScoutRepository: The inputed data isn't valid!");
                return scoutsList;
            }

            _serilogLogger.Information("ScoutRepository: Fetched some scouts by group!");
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
                _serilogLogger.Warning("ScoutRepository: The inputed data isn't valid!");
                return scoutsList;
            }

            _serilogLogger.Warning("ScoutRepository: Fetched some scouts by group!");
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
                _serilogLogger.Warning("ScoutRepository: The inputed data isn't valid!");
                return false;
            }

            try
            {
                var scout = _dbContext.Scouts.Find(scoutId);
                _serilogLogger.Information($"ScoutRepository: Searched the following scout - {scoutId}");
                return scout != null ? scout.IsActive == true : false;
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "ScoutRepository: There was a problem during the execution!");
                throw;
            }
            
        }
    }
}