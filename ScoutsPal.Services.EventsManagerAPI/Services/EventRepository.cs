using EventsPal.Services.EventsManagerAPI.Services.Interfaces;
using ScoutsPal.Services.EventsManagerAPI.DbContexts;
using ScoutsPal.Services.EventsManagerAPI.Models;
using ScoutsPal.Services.EventsManagerAPI.Services;
using Serilog;

namespace ScoutsPal.Services.EventsManagerAPI.Services
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Serilog.ILogger _serilogLogger;
        public EventRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _serilogLogger = Log.ForContext<EventRepository>();
        }

        /// <summary>
        /// Method that creates an event
        /// </summary>
        /// <param name="eventDetails"></param>
        /// <returns></returns>
        public bool CreateEvent(Event eventDetails)
        {
            if (eventDetails == null)
            {
                _serilogLogger.Warning("EventRepository: The inputed data is null");
                return false;
            }

            try
            {
                if (ExistsEvent(eventDetails.EventId))
                {
                    _serilogLogger.Warning("EventRepository: event doesn't exist!");
                    return false;
                }

                _dbContext.Events.Add(eventDetails);
                _dbContext.SaveChanges();
                _serilogLogger.Information("EventRepository: The event was created!");
                return true;
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "EventRepository: There was a problem during the execution");
                return false;
            }
        }

        /// <summary>
        /// Method that deletes and event
        /// </summary>
        /// <param name="eventDetails"></param>
        /// <returns></returns>
        public bool DeleteEvent(Event eventDetails)
        {
            if (eventDetails == null)
            {
                _serilogLogger.Warning("EventRepository: The inputed data is null");
                return false;
            }

            try
            {
                if (ExistsEvent(eventDetails.EventId))
                {
                    _serilogLogger.Warning("EventRepository: event doesn't exist!");
                    return false;
                }

                _dbContext.Events.Remove(eventDetails);
                _dbContext.SaveChanges();
                _serilogLogger.Information("EventRepository: The event was removed!");
                return true;
            }
            catch (Exception ex)
            {
                _serilogLogger.Warning(ex.Message, "EventRepository: There was a problem during the execution");
                return false;
            }
        }

        /// <summary>
        /// Method that updates an event
        /// </summary>
        /// <param name="eventDetails"></param>
        /// <returns></returns>
        public bool EditEvent(Event eventDetails)
        {
            if (eventDetails == null)
            {
                _serilogLogger.Warning("EventRepository: The inputed data is null");
                return false;
            }

            try
            {
                if (!ExistsEvent(eventDetails.EventId))
                {
                    _serilogLogger.Warning("EventRepository: The event doesn't exist!");
                    return false;
                }

                _dbContext.Events.Update(eventDetails);
                _dbContext.SaveChanges();
                _serilogLogger.Information("EventRepository: The event info was updated!");
                return true;
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "EventRepository: There was a problem during the execution");
                return false;
            }
        }

        /// <summary>
        /// Method that checks if an event exists
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool ExistsEvent(long eventId)
        {
            if (eventId <= 0)
            {
                _serilogLogger.Warning("EventRepository: The inputed data is not valid");
                return false;
            }

            _serilogLogger.Information("EventRepository: confirmed event existence!");
            return _dbContext.Events.Any(x => x.EventId == eventId);
        }

        /// <summary>
        /// Method that get's of the events
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Event> GetAllEvents()
        {
            try
            {
                return _dbContext.Events.ToList();
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "EventRepository: There was a problem during the execution!");
                return null;
            }
        }

        /// <summary>
        /// Method that gets a certain event details
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Event GetEventDetails(long eventId)
        {
            if (eventId <= 0)
            {
                _serilogLogger.Warning("EventRepository: The inputed data isn't valid!");
                return null;
            }

            try
            {
                if (!ExistsEvent(eventId))
                {
                    _serilogLogger.Warning("EventRepository: The event doesn't exist!");
                    return null;
                }

                _serilogLogger.Information("EventRepository: Fetched the event info!");
                return _dbContext.Events.FirstOrDefault(x => x.EventId == eventId);
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "EventRepository: There was a problem during the execution!");
                return null;
            }
        }

        /// <summary>
        /// Method that get's a certain group events details
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IEnumerable<Event> GetEventsByGroup(int groupId)
        {
            List<Event> scoutsList = new List<Event>();

            if (groupId <= 0)
            {
                _serilogLogger.Warning("EventRepository: The inputed data isn't valid!");
                return scoutsList;
            }

            _serilogLogger.Information("EventRepository: Fetched some events by group!");
            scoutsList = _dbContext.Events.Where(x => x.EventId == groupId).ToList();

            return scoutsList;
        }

        /// <summary>
        /// Method that checks if an event is active
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool IsActiveEvent(long eventId)
        {
            if (eventId <= 0)
            {
                _serilogLogger.Warning("EventRepository: The inputed data isn't valid!");
                return false;
            }

            try
            {
                var eventDetails = _dbContext.Events.Find(eventId);
                _serilogLogger.Information($"EventRepository: Searched the following event - {eventId}");
                return eventDetails != null ? eventDetails.IsActive == true : false;
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "EventRepository: There was a problem during the execution!");
                return false;
            }
        }
    }
}
