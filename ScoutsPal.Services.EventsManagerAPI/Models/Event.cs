using System.ComponentModel.DataAnnotations;

namespace ScoutsPal.Services.EventsManagerAPI.Models
{
    public class Event
    {
        [Key]
        public long EventId { get; set; }

        [Required]
        [MaxLength(250)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Location { get; set; }

        [Required]
        public DateTime BeginDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public decimal Cost { get; set; }

        public bool IsActive { get; set; }
    }
}
