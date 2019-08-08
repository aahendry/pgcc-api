using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PgccApi.Models;
using PgccApi.Entities;

namespace PgccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionsController : ControllerBase
    {
        private readonly PgccContext _context;

        public CompetitionsController(PgccContext context)
        {
            _context = context;
        }

        // GET: api/Competitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Competition>>> GetAll()
        {
            return await _context.Competitions.OrderBy(o => o.Name).ToListAsync();
        }

        // GET: api/Competitions/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Competition>> Get(long id)
        {
            var competition = await _context.Competitions.FindAsync(id);

            if (competition == null)
            {
                return NotFound();
            }

            return competition;
        }

        // POST: api/Competitions
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Competition>> Post(Competition item)
        {
            _context.Competitions.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        // PUT: api/Competitions/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Competition item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Competitions/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var item = await _context.Competitions.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Competitions.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}