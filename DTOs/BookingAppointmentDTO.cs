// DTOs/AppointmentBookingDto.cs
namespace REAEAA_DEPI_API.DTOs
{
    public class AppointmentBookingDto
    {
        public int DoctorID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }
}
