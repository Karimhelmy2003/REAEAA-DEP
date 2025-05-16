namespace REAEAA_DEPI_API.DTOs
{
    public class MedicalHistoryDto
    {
        public int ID { get; set; }
        public string Condition { get; set; }
        public string Treatment { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime Date { get; set; }  // ✅ Add this line
    }
}
