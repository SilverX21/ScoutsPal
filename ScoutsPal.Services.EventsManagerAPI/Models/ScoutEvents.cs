namespace ScoutsPal.Services.EventsManagerAPI.Models
{
    public class ScoutEvents
    {
        public long ScoutId { get; set; }
        public long EventId { get; set; }
        public bool HasResponded { get; set; }
        public bool ConfirmedPresence { get; set; }
    }
}
