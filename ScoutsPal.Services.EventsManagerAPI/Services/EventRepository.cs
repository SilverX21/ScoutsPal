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

        public bool CreateEvent(Event eventDetails)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEvent(Event eventDetails)
        {
            throw new NotImplementedException();
        }

        public bool EditEvent(Event eventDetails)
        {
            throw new NotImplementedException();
        }

        public bool ExistsEvent(long eventId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetAllEvents()
        {
            throw new NotImplementedException();
        }

        public Event GetEventDetails(long eventId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetEventsByGroup(int groupId)
        {
            throw new NotImplementedException();
        }

        public bool IsActiveEvent(long eventId)
        {
            throw new NotImplementedException();
        }
    }
}
