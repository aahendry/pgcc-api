using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PgccApi.Models;
using PgccApi.Entities;
using System;

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
        }

        // GET: api/Rinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RinkModel>>> GetAll(long? competitionId, long? seasonId)
        {
            if (seasonId == null)
            {
                seasonId = _context.Seasons.OrderByDescending(o => o.Name).FirstOrDefault()?.Id;

                if(seasonId == null){
                    throw new Exception("No Seasons found.");
                }
            }

            var query = _context.Rinks.Where(o => o.Season.Id == seasonId);

            if (competitionId != null)
            {
                query = query.Where(o => o.Competition.Id == competitionId);
            }

            return await query
                .OrderBy(o => o.Competition)
                .ThenBy(o => o.Skip)
                .Select(o => new RinkModel
                {
                    Id = o.Id,
                    Season = o.Season.Name,
                    Competition = o.Competition.Name,
                    Skip = o.Skip,
                    Third = o.Third,
                    Second = o.Second,
                    Lead = o.Lead
                })
                .ToListAsync();
        }

        // GET: api/Rinks/5
        [Authorize]
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
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Rink>> Post(Rink item)
        {
            // TODO - Check WasWinningRink is unique
            _context.Rinks.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        // PUT: api/Rinks/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Rink item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            // TODO - Check WasWinningRink is unique
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Rinks/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var item = await _context.Rinks.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Rinks.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Rinks/Winning
        [HttpGet("winning")]
        public async Task<ActionResult<IEnumerable<RinkModel>>> GetAllWinning(string competition)
        {
            var x = await _context.Rinks
            .Where(o => o.Competition.Name.ToLower() == competition.ToLower() && o.WasWinningRink == true)
            .OrderByDescending(o => o.Season.Name)
            .Select(o => new RinkModel
            {
                Season = o.Season.Name,
                Skip = o.Skip,
                Third = o.Third,
                Second = o.Second,
                Lead = o.Lead
            })
            .ToListAsync();

            return x;
        }
    }
}