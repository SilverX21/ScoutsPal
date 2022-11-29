using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScoutsPAl.Web.Models
{
    public class Scout
    {
       
        public long ScoutId { get; set; }

        public string Name { get; set; }
 
        public DateTime Birthday { get; set; }

        public int Age { get; set; }
    
        public string IdentificationNumber { get; set; }

        public string TaxNumber { get; set; }
  
        public string ScoutNumber { get; set; }
    
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string ResponsibleName { get; set; }

        public string ResponsiblePhoneNumber { get; set; }

        public string Address { get; set; }

        public string Photo { get; set; }

        public string Sex { get; set; }

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