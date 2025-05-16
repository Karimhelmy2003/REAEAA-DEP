using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using REAEAA_DEPI_API.Data;
using REAEAA_DEPI_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REAEAA_DEPI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Doctors
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return await _context.Doctors_REAEAA_DEPI.ToListAsync();
        }

        // GET: api/Doctors/5
        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _context.Doctors_REAEAA_DEPI.FindAsync(id);

            if (doctor == null)
                return NotFound();

            return doctor;
        }

        // POST: api/Doctors
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Doctor>> CreateDoctor(Doctor doctor)
        {
            _context.Doctors_REAEAA_DEPI.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDoctor), new { id = doctor.ID }, doctor);
        }

        // PUT: api/Doctors/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, Doctor doctor)
        {
            if (id != doctor.ID)
                return BadRequest();

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Doctors_REAEAA_DEPI.Any(d => d.ID == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Doctors/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors_REAEAA_DEPI.FindAsync(id);

            if (doctor == null)
                return NotFound();

            _context.Doctors_REAEAA_DEPI.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
