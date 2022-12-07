using ScoutsPal.Services.EventsManagerAPI.DbContexts;
using ScoutsPal.Services.EventsManagerAPI.Models;
using ScoutsPal.Services.EventsManagerAPI.Services.Interfaces;
using Serilog;

namespace ScoutsPal.Services.EventsManagerAPI.Services
{
    public class ScoutEventsRepository : IScoutEvents
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Serilog.ILogger _serilogLogger;
        public ScoutEventsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _serilogLogger = Log.ForContext<ScoutEventsRepository>();
        }

        /// <summary>
        /// Gets a list of events that a given scout was present
        /// </summary>
        /// <param name="scoutId"></param>
        /// <returns></returns>
        public IEnumerable<ScoutEvents> GetScoutEventsPresence(long scoutId)
        {
            if (scoutId <= 0)
            {
                _serilogLogger.Warning("ScoutEventsRepository: The inputed data is not valid");
                return Enumerable.Empty<ScoutEvents>();
            }

            try
            {
                var scoutPresenceList = new List<ScoutEvents>();
                scoutPresenceList = _dbContext.ScoutEvents.Where(x => x.ConfirmedPresence == true && x.ScoutId == scoutId).ToList();
                _serilogLogger.Information("ScoutEventsRepository: fetched the given scout events presence list");
                return scoutPresenceList;
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "ScoutEventsRepository: There was a problem during the execution!");
                return Enumerable.Empty<ScoutEvents>();
            }
        }

        /// <summary>
        /// Gets the scouts that confirmed their presence for a given event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public IEnumerable<ScoutEvents> GetsScoutsConfirmedPresence(long eventId)
        {
            if (eventId <= 0)
            {
                _serilogLogger.Warning("ScoutEventsRepository: The inputed data is not valid");
                return Enumerable.Empty<ScoutEvents>();
            }

            try
            {
                var eventPresenceList = new List<ScoutEvents>();
                eventPresenceList = _dbContext.ScoutEvents.Where(x => x.ConfirmedPresence == true && x.EventId == eventId).ToList();
                _serilogLogger.Information("ScoutEventsRepository: fetched the given event presence list");
                return eventPresenceList;
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "ScoutEventsRepository: There was a problem during the execution!");
                return Enumerable.Empty<ScoutEvents>();
            }
        }
    }
}
