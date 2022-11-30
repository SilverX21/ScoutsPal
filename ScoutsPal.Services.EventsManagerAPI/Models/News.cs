using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScoutsPal.Services.EventsManagerAPI.Models
{
    public class News
    {
        [Key]
        public long NewsId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(3000)]
        public string? Description { get; set; }

        //[ForeignKey]
        public long ScoutId { get; set; }
        
        [ForeignKey(nameof(Models.Event))]
        public long EventId { get; set; }

        public Event? Event { get; set; }
        public long GroupId { get; set; }
    }
}
