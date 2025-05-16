using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace REAEAA_DEPI_API.Models
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Username { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Degree { get; set; }

        [Precision(10, 2)]
        public decimal Salary { get; set; }

        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Specialization { get; set; }

        public int DeptID { get; set; }
        public int AdminID { get; set; }

        [ForeignKey("DeptID")]
        [JsonIgnore]
        public Department? Department { get; set; }  // ✅ Optional now

        [ForeignKey("AdminID")]
        [JsonIgnore]
        public Admin? Admin { get; set; }  // ✅ Optional now

        [JsonIgnore]
        public ICollection<Appointment>? Appointments { get; set; }  // ✅ Optional now
    }
}
