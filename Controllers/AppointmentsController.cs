using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REAEAA_DEPI_API.Data;
using REAEAA_DEPI_API.DTOs;
using REAEAA_DEPI_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace REAEAA_DEPI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Appointments/doctor
        [Authorize(Roles = "Doctor")]
        [HttpGet("doctor")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetDoctorAppointments()
        {
            var doctorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (doctorIdClaim == null)
                return Unauthorized("Doctor ID not found in token.");

            if (!int.TryParse(doctorIdClaim, out int doctorId))
                return BadRequest("Invalid Doctor ID.");

            var appointments = await _context.Appointments_REAEAA_DEPI
                .Where(a => a.DoctorID == doctorId)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();

            return appointments;
        }

        // GET: api/Appointments
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return await _context.Appointments_REAEAA_DEPI
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();
        }

        // GET: api/Appointments/5
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments_REAEAA_DEPI
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.ID == id);

            if (appointment == null)
                return NotFound();

            return appointment;
        }

        // POST: api/Appointments
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment(Appointment appointment)
        {
            _context.Appointments_REAEAA_DEPI.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.ID }, appointment);
        }

        // ✅ POST: api/Appointments/book (for Patients)
        [Authorize(Roles = "Patient")]
        [HttpPost("book")]
        public async Task<IActionResult> BookAppointment(AppointmentBookingDto dto)
        {
            var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Prevent double-booking:
            var conflict = await _context.Appointments_REAEAA_DEPI
                .AnyAsync(a => a.DoctorID == dto.DoctorID && a.Date == dto.Date && a.Time == dto.Time);

            if (conflict)
                return Conflict("This doctor already has an appointment at the selected date and time.");

            var adminId = await _context.Admins_REAEAA_DEPI
                .Select(a => a.ID)
                .FirstOrDefaultAsync();

            if (adminId == 0)
                return BadRequest("No admin found to assign this appointment.");

            var appointment = new Appointment
            {
                DoctorID = dto.DoctorID,
                PatientID = patientId,
                Date = dto.Date,
                Time = dto.Time,
                AdminID = adminId
            };

            _context.Appointments_REAEAA_DEPI.Add(appointment);
            await _context.SaveChangesAsync();

            return Ok("Appointment booked successfully.");
        }


        // PUT: api/Appointments/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, Appointment appointment)
        {
            if (id != appointment.ID)
                return BadRequest();

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Appointments_REAEAA_DEPI.Any(e => e.ID == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Appointments/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments_REAEAA_DEPI.FindAsync(id);
            if (appointment == null)
                return NotFound();

            _context.Appointments_REAEAA_DEPI.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Appointments/timeslots
        [Authorize(Roles = "Patient")]
        [HttpGet("timeslots")]
        public async Task<IActionResult> GetTimeSlotsByDepartment()
        {
            // Define static time slots
            var timeSlots = new List<TimeSpan>
    {
        new TimeSpan(9, 0, 0),
        new TimeSpan(10, 0, 0),
        new TimeSpan(11, 0, 0),
        new TimeSpan(12, 0, 0),
        new TimeSpan(13, 0, 0),
        new TimeSpan(14, 0, 0)
    };

            var departments = await _context.Departments_REAEAA_DEPI
                .Include(d => d.Doctors)
                .ThenInclude(doc => doc.Appointments)
                .ToListAsync();

            var result = departments.Select(dept => new
            {
                DepartmentName = dept.Name,
                TimeSlots = dept.Doctors.SelectMany(doc => timeSlots.Select(slot => new
                {
                    Time = slot.ToString(@"hh\:mm"),
                    Doctor = doc.Username,
                    Degree = doc.Degree
                }))
            });

            return Ok(result);
        }

        // GET: api/Appointments/patient
        [Authorize(Roles = "Patient")]
        [HttpGet("patient")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetPatientAppointments()
        {
            var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var appointments = await _context.Appointments_REAEAA_DEPI
                .Where(a => a.PatientID == patientId)
                .Include(a => a.Doctor)
                .Include(a => a.Doctor.Department)
                .ToListAsync();

            return appointments;
        }

        // DELETE: api/Appointments/cancel/{id}
        [Authorize(Roles = "Patient")]
        [HttpDelete("cancel/{id}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var appointment = await _context.Appointments_REAEAA_DEPI
                .FirstOrDefaultAsync(a => a.ID == id && a.PatientID == patientId);

            if (appointment == null)
                return NotFound("Appointment not found or not authorized.");

            _context.Appointments_REAEAA_DEPI.Remove(appointment);
            await _context.SaveChangesAsync();

            return Ok("Appointment cancelled successfully.");
        }

    }
}
