using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScoutsPAl.Services.ScoutsManagerAPI.Models
{
    public class Scout
    {
        [Key]
        public long ScoutId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        //Not Mapped
        [NotMapped]
        public int Age { get; set; }

        [Required]
        [MaxLength(8)]
        public string? IdentificationNumber { get; set; }

        [Required]
        [MaxLength(9)]
        public string? TaxNumber { get; set; }

        [Required]
        [MaxLength(17)]
        public string? ScoutNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(9)]
        public string? PhoneNumber { get; set; }

        [MaxLength(250)]
        public string? ResponsibleName { get; set; }

        [Required]
        [MaxLength(9)]
        public string? ResponsiblePhoneNumber { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }

        [MaxLength(250)]
        public string? Photo { get; set; }

        [Required]
        [MaxLength(10)]
        public string? Sex { get; set; }

        [Required]
        public DateTime AdmissionDate { get; set; }

        public bool IsActive { get; set; }

        public int GroupId { get; set; }
        public int ScoutTypeId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }

        public Scout()
        {
            Age = GetAge(this.Birthday);
        }

        private int GetAge(DateTime birthday)
        {
            return int.Parse((DateTime.Now.Year - birthday.Year).ToString());
        }
    }
}