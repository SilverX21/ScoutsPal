namespace ScoutsPAl.Services.ScoutsManagerAPI.Models
{
    public class ScoutType
    {
        public int ScoutTypeId { get; set; }
        public string? Descritpion { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }
}