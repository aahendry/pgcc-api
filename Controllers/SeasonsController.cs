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

namespace PgccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonsController : ControllerBase
    {
        private readonly PgccContext _context;
        private readonly IMapper _mapper;

        public SeasonsController(PgccContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Seasons/current
        [HttpGet("current")]
        public async Task<ActionResult<Season>> GetCurrent()
        {
            return await _context.Seasons.OrderByDescending(o => o.Name).FirstOrDefaultAsync();
        }

        // GET: api/Seasons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Season>>> GetAll()
        {
            return await _context.Seasons.OrderByDescending(o => o.Name).ToListAsync();
        }

        // GET: api/Seasons/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Season>> Get(long id)
        {
            var season = await _context.Seasons.FindAsync(id);

            if (season == null)
            {
                return NotFound();
            }

            return season;
        }

        // POST: api/Seasons
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Season>> Post(SeasonModel model)
        {
            var item = _mapper.Map<Season>(model);

            _context.Seasons.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.Id }, _mapper.Map<SeasonViewModel>(item));
        }

        // PUT: api/Seasons/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, SeasonModel model)
        {
            var item = await _context.Seasons.FindAsync(id);

            if (item == null)
            {
                return BadRequest();
            }

            item.Name = model.Name;

            _context.Seasons.Update(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Seasons/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var item = await _context.Seasons.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Seasons.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}