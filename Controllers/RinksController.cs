using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PgccApi.Models;

namespace PgccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RinksController : ControllerBase
    {
        private readonly PgccContext _context;

        public RinksController(PgccContext context)
        {
            _context = context;

            if (_context.Rinks.Count() == 0)
            {
                _context.Rinks.Add(new Rink { Season = "2019/2020", Competition = "Gourdie", Skip = "J Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = "2019/2020", Competition = "Gourdie", Skip = "F Gray", Third = "A Thomson", Second = "B Murray", Lead = "S Bonatti" });
                _context.Rinks.Add(new Rink { Season = "2019/2020", Competition = "Gourdie", Skip = "J Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = "2019/2020", Competition = "Gourdie", Skip = "F Gray", Third = "A Thomson", Second = "B Murray", Lead = "S Bonatti" });
                _context.Rinks.Add(new Rink { Season = "2019/2020", Competition = "Gourdie", Skip = "J Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = "2019/2020", Competition = "Gourdie", Skip = "F Gray", Third = "A Thomson", Second = "B Murray", Lead = "S Bonatti" });
                _context.Rinks.Add(new Rink { Season = "2019/2020", Competition = "Gourdie", Skip = "J Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = "2019/2020", Competition = "Gourdie", Skip = "F Gray", Third = "A Thomson", Second = "B Murray", Lead = "S Bonatti" });
                
                _context.Rinks.Add(new Rink { Season = "2019/2020", Competition = "Derby", Skip = "J Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = "2019/2020", Competition = "Derby", Skip = "F Gray", Third = "A Thomson", Second = "B Murray", Lead = "S Bonatti" });
                _context.Rinks.Add(new Rink { Season = "2019/2020", Competition = "Derby", Skip = "J Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = "2019/2020", Competition = "Derby", Skip = "F Gray", Third = "A Thomson", Second = "B Murray", Lead = "S Bonatti" });
                _context.Rinks.Add(new Rink { Season = "2019/2020", Competition = "Derby", Skip = "J Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = "2019/2020", Competition = "Derby", Skip = "F Gray", Third = "A Thomson", Second = "B Murray", Lead = "S Bonatti" });
                _context.Rinks.Add(new Rink { Season = "2019/2020", Competition = "Derby", Skip = "J Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = "2019/2020", Competition = "Derby", Skip = "F Gray", Third = "A Thomson", Second = "B Murray", Lead = "S Bonatti" });

                _context.SaveChanges();
            }
        }

        // GET: api/Rinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rink>>> GetAll(string competition)
        {
            return await _context.Rinks.Where(o => o.Competition.ToLower() == competition.ToLower()).OrderBy(o => o.Skip).ToListAsync();
        }

        // GET: api/Rinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rink>> Get(long id)
        {
            var rink = await _context.Rinks.FindAsync(id);

            if (rink == null)
            {
                return NotFound();
            }

            return rink;
        }

        // POST: api/Rinks
        [HttpPost]
        public async Task<ActionResult<Rink>> Post(Rink item)
        {
            _context.Rinks.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        // PUT: api/Rinks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Rink item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Rinks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var item = await _context.NewsItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.NewsItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}