using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectionSystem.Dtos;


namespace ElectionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ElectionDbContext _context;
        public CandidatesController(ElectionDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCandidates()
        {
            var candidates = await _context.Candidates.ToListAsync();
            return Ok(candidates);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandidateById(int id)
        {
            var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == id);

            if (candidate == null)
            {
                return NotFound(new { Message = "Candidate not found" });
            }

            return Ok(candidate);
        }
        [HttpPost]
        public async Task<IActionResult> AddCandidate(CreateCandidateDto dto)
        {
            // تحقق من صحة البيانات القادمة من dto
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // يعيد الأخطاء إذا كانت هناك حقول غير صالحة
            }

            // إنشاء كائن جديد من Candidate باستخدام الحقول من dto
            var newCandidate = new Candidate
            {
                Name = dto.Name,
                Party = dto.Party,
                Bio = dto.Bio,
                VoteCount = dto.VoteCount,
                ImagePath = dto.ImagePath
            };

            // إضافة الكائن إلى قاعدة البيانات
            await _context.Candidates.AddAsync(newCandidate);
            await _context.SaveChangesAsync();

            // إعادة الكائن المضاف مع CreatedAtAction
            return CreatedAtAction(nameof(GetCandidateById), new { id = newCandidate.Id }, newCandidate);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCandidate(int id, [FromBody] CreateCandidateDto dto)
        {
            // العثور على الكائن المطلوب تحديثه
            var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == id);

            if (candidate == null)
            {
                return NotFound(new { Message = "Candidate not found" });
            }

            // تحديث الحقول باستخدام البيانات القادمة من DTO
            candidate.Name = dto.Name ?? candidate.Name;
            candidate.Party = dto.Party ?? candidate.Party;
            candidate.Bio = dto.Bio ?? candidate.Bio;
            candidate.ImagePath = dto.ImagePath ?? candidate.ImagePath;

           

            // حفظ التغييرات
            await _context.SaveChangesAsync();

            // إعادة استجابة ناجحة
            return Ok(new { Message = "Candidate updated successfully", Candidate = candidate });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == id);

            if (candidate == null)
            {
                return NotFound(new { Message = "Candidate not found" });
            }

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Candidate deleted successfully" });
        }

    }
}
