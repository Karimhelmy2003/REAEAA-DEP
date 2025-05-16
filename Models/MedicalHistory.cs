using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace REAEAA_DEPI_API.Models
{
    public class MedicalHistory
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Condition { get; set; }

        public string Treatment { get; set; }

        public DateTime Date { get; set; }

        // Foreign key to Patient
        public int PatientID { get; set; }
        public Patient Patient { get; set; }

        // 🔧 Foreign key to Doctor
        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }  // Optional navigation
    }
}
