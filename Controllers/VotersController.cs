using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectionSystem.Models;
namespace ElectionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotersController : ControllerBase
    {
        private readonly ElectionDbContext _context;
        public VotersController(ElectionDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voter>>> GetElections()
        {
            return await _context.Voters.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetVoterById(int id)
        {
            var voter = await _context.Voters.FirstOrDefaultAsync(v => v.Id == id);

            if (voter == null)
            {
                return NotFound(new { Message = "Voter not found" });
            }

            return Ok(voter);
        }

        // 3. إضافة ناخب جديد
        [HttpPost]
        public async Task<IActionResult> AddVoter([FromBody] Voter newVoter)
        {
            // التحقق من وجود الرقم القومي مسبقًا
            if (await _context.Voters.AnyAsync(v => v.NationalId == newVoter.NationalId))
            {
                return BadRequest(new { Message = "Voter with this National ID already exists" });
            }

            await _context.Voters.AddAsync(newVoter);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVoterById), new { id = newVoter.Id }, newVoter);
        }

        // 5. حذف ناخب
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoter(int id)
        {
            var voter = await _context.Voters.FirstOrDefaultAsync(v => v.Id == id);

            if (voter == null)
            {
                return NotFound(new { Message = "Voter not found" });
            }

            _context.Voters.Remove(voter);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Voter deleted successfully" });
        }



    }
}
