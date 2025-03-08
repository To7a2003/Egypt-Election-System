using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectionSystem.Dtos;  // تأكد من استخدام الـ DTO المناسب
using ElectionSystem.Models;  // تأكد من أن الـ namespace هنا يتطابق مع المسار الصحيح

namespace ElectionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ElectionDbContext _context;

        public AuthController(ElectionDbContext context)
        {
            _context = context;
        }

        // API لتسجيل الدخول
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // التحقق إذا كان البريد الإلكتروني هو بريد Admin
            if (loginDto.Email.ToLower() == "admin@elections.com") // ايمييل الـ Admin
            {
                var adminUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

                if (adminUser == null || adminUser.Password != loginDto.Password) // تأكد من تحسين الأمان باستخدام التشفير
                    return Unauthorized(new { Message = "Invalid credentials" });

                return Ok(new { Message = "Login successful as Admin", Role = adminUser.Role });
            }

            // التحقق من المستخدم العادي (Voter)
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || user.Password != loginDto.Password) // تأكد من تحسين الأمان باستخدام التشفير
                return Unauthorized(new { Message = "Invalid email or password." });

            return Ok(new { Message = "Login successful as Voter", Role = user.Role });
        }

        // API للتسجيل (Register)
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // التحقق إذا كان البريد الإلكتروني موجودًا مسبقًا
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == registerDto.Email);

            if (existingUser != null)
                return Conflict(new { Message = "Email already in use." });

            // إنشاء مستخدم جديد
            var newUser = new User
            {
                Email = registerDto.Email,
                Password = registerDto.Password,  // يمكن تشفير كلمة المرور هنا
                Role = "Voter",  // أو Admin بناء على بياناتك
                Voter = new Voter
                {
                    Name = registerDto.Name,
                    NationalId = registerDto.NationalId,
                    DateOfBirth = registerDto.DateOfBirth,
                    ElectionType = registerDto.ElectionType,
                     NationalIdPhoto= registerDto.NationalIdPhoto,
                    SelfiePhoto= registerDto.SelfiePhoto
                }
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Registration successful", User = newUser });
        }
    }
}
