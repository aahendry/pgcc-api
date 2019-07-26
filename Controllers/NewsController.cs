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
    public class NewsController : ControllerBase
    {
        private readonly PgccContext _context;

        public NewsController(PgccContext context)
        {
            _context = context;

            if (_context.NewsItems.Count() == 0)
            {
                _context.NewsItems.Add(new NewsItem { Title = "Example Title", Text = "Example Text", IsVisible = true });
                _context.SaveChanges();
            }
        }

        // GET: api/News
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsItem>>> GetAll()
        {
            return await _context.NewsItems.ToListAsync();
        }

        // GET: api/News/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsItem>> Get(long id)
        {
            var newsItem = await _context.NewsItems.FindAsync(id);

            if (newsItem == null)
            {
                return NotFound();
            }

            return newsItem;
        }

        // POST: api/News
        [HttpPost]
        public async Task<ActionResult<NewsItem>> Post(NewsItem item)
        {
            _context.NewsItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        // PUT: api/News/5
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