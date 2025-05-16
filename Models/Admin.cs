using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace REAEAA_DEPI_API.Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public ICollection<Patient> Patients { get; set; }

        [JsonIgnore]
        public ICollection<Doctor> Doctors { get; set; }

        [JsonIgnore]
        public ICollection<Appointment> Appointments { get; set; }
    }
}
