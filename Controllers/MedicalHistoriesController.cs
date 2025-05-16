using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REAEAA_DEPI_API.Data;
using REAEAA_DEPI_API.Models;
using REAEAA_DEPI_API.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace REAEAA_DEPI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicalHistoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedicalHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MedicalHistories
        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalHistoryDto>>> GetMedicalHistories()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var query = _context.MedicalHistories_REAEAA_DEPI.AsQueryable();

            if (role == "Doctor")
            {
                query = query.Where(m => m.DoctorID == userId);
            }

            return await query.Select(m => new MedicalHistoryDto
            {
                ID = m.ID,
                Condition = m.Condition,
                Treatment = m.Treatment,
                DoctorID = m.DoctorID,
                PatientID = m.PatientID,
                Date = m.Date
            }).ToListAsync();
        }

        // GET: api/MedicalHistories/5
        [Authorize(Roles = "Admin,Doctor,Patient")]
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalHistoryDto>> GetMedicalHistory(int id)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var m = await _context.MedicalHistories_REAEAA_DEPI.FirstOrDefaultAsync(m => m.ID == id);
            if (m == null)
                return NotFound();

            if ((role == "Doctor" && m.DoctorID != userId) || (role == "Patient" && m.PatientID != userId))
                return Forbid();

            return new MedicalHistoryDto
            {
                ID = m.ID,
                Condition = m.Condition,
                Treatment = m.Treatment,
                DoctorID = m.DoctorID,
                PatientID = m.PatientID,
                Date = m.Date
            };
        }

        // POST: api/MedicalHistories
        [Authorize(Roles = "Admin,Doctor")]
        [HttpPost]
        public async Task<ActionResult<MedicalHistoryDto>> PostMedicalHistory(MedicalHistoryDto dto)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var entity = new MedicalHistory
            {
                Condition = dto.Condition,
                Treatment = dto.Treatment,
                PatientID = dto.PatientID,
                DoctorID = (role == "Doctor") ? userId : dto.DoctorID,
                Date = DateTime.Now
            };

            _context.MedicalHistories_REAEAA_DEPI.Add(entity);
            await _context.SaveChangesAsync();

            dto.ID = entity.ID;
            dto.DoctorID = entity.DoctorID;
            dto.Date = entity.Date;

            return CreatedAtAction(nameof(GetMedicalHistory), new { id = dto.ID }, dto);
        }

        // PUT: api/MedicalHistories/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalHistory(int id, MedicalHistoryDto dto)
        {
            if (id != dto.ID)
                return BadRequest();

            var entity = await _context.MedicalHistories_REAEAA_DEPI.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.Condition = dto.Condition;
            entity.Treatment = dto.Treatment;
            entity.DoctorID = dto.DoctorID;
            entity.PatientID = dto.PatientID;
            entity.Date = dto.Date;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/MedicalHistories/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalHistory(int id)
        {
            var entity = await _context.MedicalHistories_REAEAA_DEPI.FindAsync(id);
            if (entity == null)
                return NotFound();

            _context.MedicalHistories_REAEAA_DEPI.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
