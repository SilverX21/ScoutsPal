using ScoutsPal.Services.EventsManagerAPI.Models;

namespace EventsPal.Services.EventsManagerAPI.Services.Interfaces
{
    public interface IEventRepository
    {
        public IEnumerable<Event> GetEventsByGroup(int groupId);

        public IEnumerable<Event> GetAllEvents();

        public Event GetEventDetails(long eventId);

        public bool CreateEvent(Event eventDetails);

        public bool EditEvent(Event eventDetails);

        public bool DeleteEvent(Event eventDetails);

        public bool IsActiveEvent(long eventId);

        public bool ExistsEvent(long eventId);
    }
}
