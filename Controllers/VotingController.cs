using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotingController : ControllerBase
    {
        private readonly ElectionDbContext _context;

        public VotingController(ElectionDbContext context)
        {
            _context = context;
        }

        // دالة عرض المرشحين
        [HttpGet]
        public IActionResult Index()
        {
            var candidates = _context.Candidates.ToList();  // جلب جميع المرشحين
            return Ok(candidates);
        }

        // دالة التصويت (POST)
        [HttpPost]
        public IActionResult Vote(int candidateId)
        {
            // البحث عن المرشح الذي قام المستخدم بالتصويت له
            var candidate = _context.Candidates.Find(candidateId);

            // إذا لم يتم العثور على المرشح، عرض صفحة الخطأ
            if (candidate == null)
            {
                return NotFound();
            }

            // زيادة عدد الأصوات للمرشح
            candidate.VoteCount += 1;

            // حفظ التغييرات في قاعدة البيانات
            _context.SaveChanges();

            // إعادة التوجيه لعرض نتائج الانتخابات
            return RedirectToAction("ElectionResults");
        }

        // دالة عرض نتائج الانتخابات
        [HttpGet("results")]
        public IActionResult ElectionResults()
        {
            var results = _context.Candidates.ToList();  // جلب جميع المرشحين مع عدد الأصوات
            return Ok(results);
        }
    }
}
