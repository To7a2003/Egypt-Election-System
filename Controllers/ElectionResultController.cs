using ElectionSystem.Dtos;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectionResultController : ControllerBase
    {
        private readonly ElectionDbContext _context;

        public ElectionResultController(ElectionDbContext context)
        {
            _context = context;
        }

        // دالة لعرض نتائج الانتخابات
        [HttpGet]
        public async Task<IActionResult> GetResults()
        {
            // جلب بيانات المرشحين وعدد الأصوات
            var candidates = await _context.Candidates
                .Include(c => c.Election)  // إذا كنت بحاجة لإدراج بيانات الانتخابات
                .OrderByDescending(c => c.VoteCount)
                .Select(c => new ResultDto
                {
                    CandidateId = c.Id,
                    CandidateName = c.Name,
                    VoteCount = c.VoteCount,
                    ElectionId = c.Election.Id,
                    ElectionName = c.Election.Name
                })
                .ToListAsync();

            if (candidates.Count == 0)
            {
                return NotFound(new { Message = "No candidates found" });  // في حال عدم وجود مرشحين
            }

            // إرجاع البيانات بتنسيق JSON
            return Ok(candidates);
        }
    }
}
