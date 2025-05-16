namespace REAEAA_DEPI_API.DTOs
{
    public class RegisterRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Admin", "Doctor", or "Patient"
    }
}
