using System.ComponentModel.DataAnnotations;

namespace ScoutsPAl.Services.ScoutsManagerAPI.Models
{
    public class Scout
    {
        [Key]
        public long ScoutId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        //Not Mapped
        public int Age { get; set; }

        [Required]
        [MaxLength(12)]
        public string IdentificationNumber { get; set; }

        [Required]
        [MaxLength(9)]
        public string TaxNumber { get; set; }

        [Required]
        [MaxLength(9)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(9)]
        public string ParentPhoneNumber { get; set; }

        [MaxLength(250)]
        public string ParentName { get; set; }

        [Required]
        [MaxLength(250)]
        public string Email { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        public string Photo { get; set; }

        [Required]
        [MaxLength(15)]
        public string Sex { get; set; }

        [Required]
        public DateTime AdmissionDate { get; set; }

        public bool Active { get; set; }

        public int GroupId { get; set; }
        public int RoleId { get; set; }
        public int ScoutTypeId { get; set; }

        public Scout()
        {
        }

        private int GetAge(DateTime birthday)
        {
            return int.Parse((DateTime.Now.Year - birthday.Year).ToString());
        }
    }
}