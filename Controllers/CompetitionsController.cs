using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PgccApi.Models;
using PgccApi.Entities;
using AutoMapper;
using PgccApi.Models.ViewModels;

namespace PgccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionsController : ControllerBase
    {
        private readonly PgccContext _context;
        private readonly IMapper _mapper;

        public CompetitionsController(PgccContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Competitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompetitionViewModel>>> GetAll()
        {
            var query = _context.Competitions.OrderBy(o => o.Name);

            return await _mapper.ProjectTo<CompetitionViewModel>(query).ToListAsync();
        }

        // GET: api/Competitions/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<CompetitionViewModel>> Get(long id)
        {
            var competition = await _context.Competitions.FindAsync(id);

            if (competition == null)
            {
                return NotFound();
            }

            return _mapper.Map<CompetitionViewModel>(competition);
        }

        // POST: api/Competitions
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CompetitionViewModel>> Post(CompetitionPostModel model)
        {
            var item = _mapper.Map<Competition>(model);

            _context.Competitions.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.Id }, _mapper.Map<CompetitionViewModel>(item));
        }

        // PUT: api/Competitions/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, CompetitionPutModel model)
        {
            var item = await _context.Competitions.FindAsync(id);

            if(item == null)
            {
                return BadRequest();
            }

            item.Name = model.Name;
            item.Blurb = model.Blurb;

            _context.Competitions.Update(item);
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