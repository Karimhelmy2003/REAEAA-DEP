using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace REAEAA_DEPI_API.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }
        public int FloorNo { get; set; }

        [JsonIgnore]
        public ICollection<Doctor>? Doctors { get; set; }  // ✅ Make this optional
    }
}
