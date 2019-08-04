using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
    public class RinksController : ControllerBase
    {
        private readonly PgccContext _context;

        public RinksController(PgccContext context)
        {
            _context = context;

            if (_context.Rinks.Count() == 0)
            {
                var season = new Season { Name = "2019/2020" };
                _context.Seasons.Add(season);
                var gourdie = new Competition { Name = "Gourdie" };
                var derby = new Competition { Name = "Derby" };
                _context.Competitions.Add(gourdie);
                _context.Competitions.Add(derby);

                _context.Rinks.Add(new Rink { Season = season, Competition = gourdie, Skip = "J Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = season, Competition = gourdie, Skip = "F Gray", Third = "A Thomson", Second = "B Murray", Lead = "S Bonatti" });
                _context.Rinks.Add(new Rink { Season = season, Competition = gourdie, Skip = "J Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = season, Competition = gourdie, Skip = "F Gray", Third = "A Thomson", Second = "B Murray", Lead = "S Bonatti" });
                _context.Rinks.Add(new Rink { Season = season, Competition = gourdie, Skip = "J Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = season, Competition = gourdie, Skip = "F Gray", Third = "A Thomson", Second = "B Murray", Lead = "S Bonatti" });
                _context.Rinks.Add(new Rink { Season = season, Competition = gourdie, Skip = "J Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = season, Competition = gourdie, Skip = "F Gray", Third = "A Thomson", Second = "B Murray", Lead = "S Bonatti" });

                _context.Rinks.Add(new Rink { Season = season, Competition = derby, Skip = "J Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = season, Competition = derby, Skip = "F Gray", Third = "A Thomson", Second = "B Murray", Lead = "S Bonatti" });
                _context.Rinks.Add(new Rink { Season = season, Competition = derby, Skip = "J Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = season, Competition = derby, Skip = "F Gray", Third = "A Thomson", Second = "B Murray", Lead = "S Bonatti" });
                _context.Rinks.Add(new Rink { Season = season, Competition = derby, Skip = "J Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = season, Competition = derby, Skip = "F Gray", Third = "A Thomson", Second = "B Murray", Lead = "S Bonatti" });
                _context.Rinks.Add(new Rink { Season = season, Competition = derby, Skip = "J Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = season, Competition = derby, Skip = "F Gray", Third = "A Thomson", Second = "B Murray", Lead = "S Bonatti" });
                
                _context.Rinks.Add(new Rink { Season = new Season { Name = "2016/2017" }, WasWinningRink = true, Competition = gourdie, Skip = "C Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = new Season { Name = "2017/2018" }, WasWinningRink = true, Competition = gourdie, Skip = "B Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                _context.Rinks.Add(new Rink { Season = new Season { Name = "2018/2019" }, WasWinningRink = true, Competition = gourdie, Skip = "A Service", Third = "R Leishman", Second = "N Caskie", Lead = "J Taylor" });
                
                _context.SaveChanges();
            }
        }

        // GET: api/Rinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rink>>> GetAll(string competition, long? season = null)
        {
            if (season == null)
            {
                season = _context.Seasons.OrderByDescending(o => o.Name).FirstOrDefault()?.Id;
            }

            return await _context.Rinks.Where(o => o.Season.Id == season).Where(o => o.Competition.Name.ToLower() == competition.ToLower()).OrderBy(o => o.Skip).ToListAsync();
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

        // GET: api/Rinks/Winning
        [Authorize]
        [HttpGet("winning")]
        public async Task<ActionResult<IEnumerable<Rink>>> GetAllWinning(string competition)
        {
            return await _context.Rinks
            .Where(o => o.Competition.Name.ToLower() == competition.ToLower())
            .Where(o => o.WasWinningRink == true)
            .OrderByDescending(o => o.Season)
            .ToListAsync();
        }
    }
}