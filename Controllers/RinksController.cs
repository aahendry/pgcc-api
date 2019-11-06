using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PgccApi.Models;
using PgccApi.Models.ViewModels;
using PgccApi.Entities;
using System;
using AutoMapper;

namespace PgccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RinksController : ControllerBase
    {
        private readonly PgccContext _context;
        private readonly IMapper _mapper;

        public RinksController(PgccContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Rinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RinkViewModel>>> GetAll(long? competitionId, long? seasonId)
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

            var rinks = query.Include(o => o.Competition).Include(o => o.Season)
                .OrderBy(o => o.Competition.Name)
                .ThenBy(o => o.Skip);

            var rinksList = rinks.ToList();

            var x = await _mapper.ProjectTo<RinkViewModel>(rinks).ToListAsync();

            return x;
        }

        // GET: api/Rinks/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<RinkViewModel>> Get(long id)
        {
            var rink = await _context.Rinks.Include(o => o.Competition).Include(o => o.Season).FirstOrDefaultAsync(o => o.Id == id);

            if (rink == null)
            {
                return NotFound();
            }

            return _mapper.Map<RinkViewModel>(rink);
        }

        // POST: api/Rinks
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RinkViewModel>> Post(Rink item)
        {
            EnsureOnlyOneWinningRink(item);

            _context.Rinks.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.Id }, _mapper.Map<RinkViewModel>(item));
        }

        private void EnsureOnlyOneWinningRink(Rink item)
        {
            var existingWinningRinks = _context.Rinks
                .Where(o =>
                    o.WasWinningRink == true &&
                    o.SeasonId == item.SeasonId &&
                    o.CompetitionId == item.CompetitionId)
                .Count();

            if(existingWinningRinks > 0)
            {
                throw new Exception("Already a winning rink for this season and competition.");
            }
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

            EnsureOnlyOneWinningRink(item);

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
        public async Task<ActionResult<IEnumerable<RinkViewModel>>> GetAllWinning(string competition)
        {
            var rinks = _context.Rinks
                .Where(o => o.Competition.Name.ToLower() == competition.ToLower() && o.WasWinningRink == true)
                .OrderByDescending(o => o.Season.Name);

            return await _mapper.ProjectTo<RinkViewModel>(rinks).ToListAsync();
        }
    }
}