using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REAEAA_DEPI_API.Data;
using REAEAA_DEPI_API.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace REAEAA_DEPI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Account/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterPatient([FromBody] Patient patient)
        {
            if (string.IsNullOrEmpty(patient.Username) || string.IsNullOrEmpty(patient.Password))
                return BadRequest("Username and Password are required.");

            _context.Patients_REAEAA_DEPI.Add(patient);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Patient registered successfully." });
        }

        // GET: api/Account/profile
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (role == "Admin")
                return Ok(await _context.Admins_REAEAA_DEPI.FirstOrDefaultAsync(a => a.ID == id));

            if (role == "Doctor")
                return Ok(await _context.Doctors_REAEAA_DEPI.FirstOrDefaultAsync(d => d.ID == id));

            if (role == "Patient")
                return Ok(await _context.Patients_REAEAA_DEPI.FirstOrDefaultAsync(p => p.ID == id));

            return Unauthorized("Role not recognized.");
        }
    }
}
