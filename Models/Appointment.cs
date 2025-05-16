using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace REAEAA_DEPI_API.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int AdminID { get; set; }

        [ForeignKey("PatientID")]
        [JsonIgnore]
        public Patient? Patient { get; set; }

        [ForeignKey("DoctorID")]
        [JsonIgnore]
        public Doctor? Doctor { get; set; }

        [ForeignKey("AdminID")]
        [JsonIgnore]
        public Admin? Admin { get; set; }
    }
}
