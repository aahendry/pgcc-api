using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PgccApi.Models;
using PgccApi.Entities;
using PgccApi.Models.ViewModels;
using AutoMapper;
using System;

namespace PgccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixturesController : ControllerBase
    {
        private readonly PgccContext _context;
        private readonly IMapper _mapper;

        public FixturesController(PgccContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Fixtures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FixtureViewModel>>> GetAll(long? seasonId)
        {
            if (seasonId == null)
            {
                seasonId = _context.Seasons.OrderByDescending(o => o.Name).FirstOrDefault()?.Id;

                if (seasonId == null)
                {
                    throw new Exception("No Seasons found.");
                }
            }

            var fixtures = _context.Fixtures.Include(o => o.Team1).Include(o => o.Team2).Include(o => o.Competition).Include(o => o.Season)
                .Where(o => o.SeasonId == seasonId).OrderBy(o => o.When);

            return await _mapper.ProjectTo<FixtureViewModel>(fixtures).ToListAsync();
        }

        // GET: api/Fixtures/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<FixtureViewModel>> Get(long id)
        {
            var fixture = await _context.Fixtures.FindAsync(id);

            return _mapper.Map<FixtureViewModel>(fixture);
        }

        // POST: api/Fixtures
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<FixtureViewModel>> Post(Fixture item)
        {
            _context.Fixtures.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.Id }, _mapper.Map<FixtureViewModel>(item));
        }

        // POST: api/Fixtures/5
        [Authorize]
        [HttpPost("{id}")]
        public async Task<ActionResult<FixtureViewModel>> PostCopy(long id)
        {
            var item = await _context.Fixtures
                .Include(o => o.Competition)
                .Include(o => o.Season)
                .Include(o => o.Team1)
                .Include(o => o.Team2)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (item == null)
            {
                return BadRequest();
            }

            var newItem = new Fixture
            {
                SeasonId = item.SeasonId,
                CompetitionId = item.CompetitionId,
                Team1Id = item.Team1Id,
                Team2Id = item.Team2Id,
                Team1OtherName = item.Team1OtherName,
                Team2OtherName = item.Team2OtherName,
                Shots1 = item.Shots1,
                Shots2 = item.Shots2,
                Ends1 = item.Ends1,
                Ends2 = item.Ends2,
                When = item.When,
                Round = item.Round,
                IsFinal = item.IsFinal,
                ManOfTheMatch = item.ManOfTheMatch
            };

            _context.Fixtures.Add(newItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = newItem.Id }, _mapper.Map<FixtureViewModel>(newItem));
        }

        // PUT: api/Fixtures/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Fixture model)
        {
            var item = await _context.Fixtures.Include(o => o.Competition).FirstOrDefaultAsync(o => o.Id == id);

            if (item == null)
            {
                return BadRequest();
            }

            item.When = model.When;
            item.Round = model.Round;
            item.IsFinal = model.IsFinal;
            item.ManOfTheMatch = model.ManOfTheMatch;
            item.Shots1 = model.Shots1;
            item.Shots2 = model.Shots2;
            item.Ends1 = model.Ends1;
            item.Ends2 = model.Ends2;

            if (item.Competition.HasLeagueTable)
            {
                item.Team1Id = model.Team1Id;
                item.Team2Id = model.Team2Id;
                item.Team1OtherName = null;
                item.Team2OtherName = null;
            }
            else
            {
                item.Team1Id = null;
                item.Team2Id = null;
                item.Team1OtherName = model.Team1OtherName;
                item.Team2OtherName = model.Team2OtherName;
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Fixtures/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var item = await _context.Fixtures.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Fixtures.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}