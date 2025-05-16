using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REAEAA_DEPI_API.Data;
using REAEAA_DEPI_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REAEAA_DEPI_API.Controllers
{
    [Authorize(Roles = "Admin")] // 🔐 All department actions require Admin role
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Departments_REAEAA_DEPI.ToListAsync();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Departments_REAEAA_DEPI.FindAsync(id);

            if (department == null)
                return NotFound();

            return department;
        }

        // POST: api/Departments
        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment(Department department)
        {
            _context.Departments_REAEAA_DEPI.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartment), new { id = department.ID }, department);
        }

        // PUT: api/Departments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department department)
        {
            if (id != department.ID)
                return BadRequest();

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Departments_REAEAA_DEPI.Any(d => d.ID == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments_REAEAA_DEPI.FindAsync(id);

            if (department == null)
                return NotFound();

            _context.Departments_REAEAA_DEPI.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
