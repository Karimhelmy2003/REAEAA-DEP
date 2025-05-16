using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REAEAA_DEPI_API.Data;
using REAEAA_DEPI_API.Models;

namespace REAEAA_DEPI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            return await _context.Admins_REAEAA_DEPI
.ToListAsync();
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _context.Admins_REAEAA_DEPI
.FindAsync(id);
            if (admin == null)
                return NotFound();

            return admin;
        }

        // ✅ LOGIN will be implemented separately in an AuthController
        // ❌ Do not expose POST, PUT, DELETE for Admins to the public
    }
}
