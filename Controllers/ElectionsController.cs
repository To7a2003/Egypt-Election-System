using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectionSystem.Dtos; // يجب تضمين مساحة الأسماء الخاصة بـ DTO

namespace ElectionsSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectionsController : ControllerBase
    {
        private readonly ElectionDbContext _context;

        public ElectionsController(ElectionDbContext context)
        {
            _context = context;
        }

        // GET: api/elections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElectionDto>>> GetElections()
        {
            var elections = await _context.Elections.ToListAsync();
            var electionDtos = elections.Select(e => new ElectionDto
            {
                Name = e.Name,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                ElectionType = e.ElectionType
            }).ToList();

            return Ok(electionDtos);
        }

        // GET: api/elections/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ElectionDto>> GetElection(int id)
        {
            var election = await _context.Elections.FindAsync(id);

            if (election == null)
            {
                return NotFound();
            }

            var electionDto = new ElectionDto
            {
                Name = election.Name,
                StartDate = election.StartDate,
                EndDate = election.EndDate,
                ElectionType = election.ElectionType
            };

            return Ok(electionDto);
        }

        // POST: api/elections
        [HttpPost]
        public async Task<ActionResult<Election>> PostElection(ElectionDto electionDto)
        {
            var election = new Election
            {
                Name = electionDto.Name,
                StartDate = electionDto.StartDate,
                EndDate = electionDto.EndDate,
                ElectionType = electionDto.ElectionType
            };

            _context.Elections.Add(election);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetElection", new { id = election.Id }, election);
        }

        // PUT: api/elections/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateElection(int id, ElectionDto electionDto)
        {
            if (id != electionDto.Id)
            {
                return NotFound($"{id} is not found");
            }

            var election = await _context.Elections.FindAsync(id);
            if (election == null)
            {
                return NotFound();
            }

            election.Name = electionDto.Name;
            election.StartDate = electionDto.StartDate;
            election.EndDate = electionDto.EndDate;
            election.ElectionType = electionDto.ElectionType;

            _context.Entry(election).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/elections/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElection(int id)
        {
            var election = await _context.Elections.FindAsync(id);
            if (election == null)
            {
                return NotFound();
            }

            _context.Elections.Remove(election);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

