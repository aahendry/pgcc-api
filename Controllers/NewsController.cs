using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using PgccApi.Models;
using PgccApi.Entities;
using PgccApi.Models.ViewModels;
using AutoMapper;

namespace PgccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly PgccContext _context;
        private readonly IMapper _mapper;

        public NewsController(PgccContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/News
        [HttpGet("visible")]
        public async Task<ActionResult<IEnumerable<NewsItemViewModel>>> GetAllVisible()
        {
            var query = _context.NewsItems.Where(o => o.IsVisible).OrderByDescending(o => o.When);

            return await _mapper.ProjectTo<NewsItemViewModel>(query).ToListAsync();
        }

        // GET: api/News
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsItemViewModel>>> GetAll()
        {
            var query = _context.NewsItems.OrderByDescending(o => o.When);

            return await _mapper.ProjectTo<NewsItemViewModel>(query).ToListAsync();
        }

        // GET: api/News/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsItemViewModel>> Get(long id)
        {
            var newsItem = await _context.NewsItems.FindAsync(id);

            if (newsItem == null)
            {
                return NotFound();
            }

            return _mapper.Map<NewsItemViewModel>(newsItem);
        }

        // POST: api/News
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<NewsItemViewModel>> Post(NewsItem item)
        {
            item.When = DateTime.UtcNow;
            _context.NewsItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.Id }, _mapper.Map<NewsItemViewModel>(item));
        }

        // PUT: api/News/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, NewsItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/News/5
        [Authorize]
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