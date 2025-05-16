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
    [Authorize(Roles = "Admin")] // 🔐 All actions require an Admin token
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            return await _context.Patients_REAEAA_DEPI.ToListAsync();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.Patients_REAEAA_DEPI.FindAsync(id);

            if (patient == null)
                return NotFound();

            return patient;
        }

        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
        {
            _context.Patients_REAEAA_DEPI.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPatient), new { id = patient.ID }, patient);
        }

        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            if (id != patient.ID)
                return BadRequest();

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Patients_REAEAA_DEPI.Any(p => p.ID == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients_REAEAA_DEPI.FindAsync(id);

            if (patient == null)
                return NotFound();

            _context.Patients_REAEAA_DEPI.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
