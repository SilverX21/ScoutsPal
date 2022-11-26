using Microsoft.OpenApi.Writers;
using ScoutsPAl.Services.ScoutsManagerAPI.DbContexts;
using ScoutsPAl.Services.ScoutsManagerAPI.Models;
using ScoutsPAl.Services.ScoutsManagerAPI.Services.Interfaces;
using Serilog;
using static System.Formats.Asn1.AsnWriter;

namespace ScoutsPAl.Services.ScoutsManagerAPI.Services
{
    public class ScoutTypeRepository : IScoutTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Serilog.ILogger _serilogLogger;

        public ScoutTypeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _serilogLogger = Log.ForContext<ScoutTypeRepository>();
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
                _serilogLogger.Warning("ScoutTypeRepository: The inputed data is null");
                return false;
            }

            try
            {
                if (ExistsScoutType(scoutType.ScoutTypeId))
                {
                    _serilogLogger.Warning("ScoutTypeRepository: the scout type doesn't exist!");
                    return false;
                }

                _dbContext.ScoutTypes.Add(scoutType);
                _dbContext.SaveChanges();
                _serilogLogger.Information("ScoutTypeRepository: The scout type was created!");
                return true;
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "ScoutTypeRepository: There was a problem during the execution");
                return false;
            }
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
                _serilogLogger.Warning("ScoutTypeRepository: The inputed data is null");
                return false;
            }

            try
            {
                if (!ExistsScoutType(scoutType.ScoutTypeId))
                {
                    _serilogLogger.Warning("ScoutTypeRepository: The scout type doesn't exist!");
                    return false;
                }

                _dbContext.ScoutTypes.Remove(scoutType);
                _dbContext.SaveChanges();
                _serilogLogger.Information("ScoutTypeRepository: The scout type was deleted!");
                return true;
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "ScoutTypeRepository: There was a problem during the execution");
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
                _serilogLogger.Warning("ScoutTypeRepository: The inputed data is null");
                return false;
            }

            try
            {
                if (!ExistsScoutType(scoutType.ScoutTypeId))
                {
                    _serilogLogger.Warning("ScoutTypeRepository: The scout type doesn't exist!");
                    return false;
                }

                _dbContext.ScoutTypes.Update(scoutType);
                _dbContext.SaveChanges();
                _serilogLogger.Information("ScoutTypeRepository: The scout type was updated!");
                return true;
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "ScoutTypeRepository: There was a problem during the execution");
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
                _serilogLogger.Warning("ScoutTypeRepository: The inputed data is invalid");
                return false;
            }

            _serilogLogger.Information("ScoutTypeRepository: The scout type was fetched!");
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
                _serilogLogger.Information("ScoutTypeRepository: It was fetched some scout type data!");
                return _dbContext.ScoutTypes.ToList();
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "ScoutTypeRepository: There was a problem during the execution");
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
                _serilogLogger.Warning("ScoutTypeRepository: The inputed data is invalid");
                return null;
            }

            try
            {
                if (!ExistsScoutType(scoutTypeId))
                {
                    _serilogLogger.Warning("ScoutTypeRepository: The scout type doesn't exist!");
                    return null;
                }

                _serilogLogger.Information("ScoutTypeRepository: The scout type details were fetched!");
                return _dbContext.ScoutTypes.FirstOrDefault(x => x.ScoutTypeId == scoutTypeId);
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "ScoutTypeRepository: There was a problem during the execution");
                return null;
            }
        }
    }
}