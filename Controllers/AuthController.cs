using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using REAEAA_DEPI_API.Data;
using REAEAA_DEPI_API.DTOs;
using REAEAA_DEPI_API.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace REAEAA_DEPI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtSettings _jwtSettings;

        public AuthController(ApplicationDbContext context, IOptions<JwtSettings> jwtOptions)
        {
            _context = context;
            _jwtSettings = jwtOptions.Value;
        }

        [HttpPost("login/admin")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Username and password are required.");

            var admin = await _context.Admins_REAEAA_DEPI
                .FirstOrDefaultAsync(a => a.Username == request.Username && a.Password == request.Password);
            if (admin != null)
                return Ok(GenerateToken("Admin", admin.ID));

            return Unauthorized("Invalid admin credentials.");
        }

        [HttpPost("login/doctor")]
        public async Task<IActionResult> LoginDoctor([FromBody] LoginRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Username and password are required.");

            var doctor = await _context.Doctors_REAEAA_DEPI
                .FirstOrDefaultAsync(d => d.Username == request.Username && d.Password == request.Password);
            if (doctor != null)
                return Ok(GenerateToken("Doctor", doctor.ID));

            return Unauthorized("Invalid doctor credentials.");
        }

        [HttpPost("login/patient")]
        public async Task<IActionResult> LoginPatient([FromBody] LoginRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Username and password are required.");

            var patient = await _context.Patients_REAEAA_DEPI
                .FirstOrDefaultAsync(p => p.Username == request.Username && p.Password == request.Password);
            if (patient != null)
                return Ok(GenerateToken("Patient", patient.ID));

            return Unauthorized("Invalid patient credentials.");
        }

        private object GenerateToken(string role, int userId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),                  // ✅ Added
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),                    // Keep for compatibility
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
                signingCredentials: creds);

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                role = role
            };
        }
    }
}
