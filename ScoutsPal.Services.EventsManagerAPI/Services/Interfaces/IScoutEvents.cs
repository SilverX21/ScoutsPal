using ScoutsPal.Services.EventsManagerAPI.Models;

namespace ScoutsPal.Services.EventsManagerAPI.Services.Interfaces
{
    public interface IScoutEvents
    {
        public IEnumerable<ScoutEvents> GetScoutEventsPresence(long scoutId);

        public IEnumerable<ScoutEvents> GetsScoutsConfirmedPresence(long eventId);
    }
}
