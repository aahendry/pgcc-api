using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using PgccApi.Models;
using PgccApi.Entities;

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
                _context.NewsItems.Add(new NewsItem { Title = "News Item 1", Text = "10 days ago, something happened...", When = DateTime.UtcNow.AddDays(-10), IsVisible = true });
                _context.NewsItems.Add(new NewsItem { Title = "News Item 2", Text = "9 days ago, something happened...", When = DateTime.UtcNow.AddDays(-9), IsVisible = true });
                _context.NewsItems.Add(new NewsItem { Title = "News Item 3", Text = "8 days ago, something happened...", When = DateTime.UtcNow.AddDays(-8), IsVisible = true });
                _context.NewsItems.Add(new NewsItem { Title = "News Item 4", Text = "A week ago, some stuff happened...", When = DateTime.UtcNow.AddDays(-7), IsVisible = true });
                _context.NewsItems.Add(new NewsItem { Title = "News Item 5", Text = "4 days ago, some more stuff happened...", When = DateTime.UtcNow.AddDays(-4), IsVisible = true });
                _context.NewsItems.Add(new NewsItem { Title = "News Item 6", Text = "Yesterday, even more stuff happened...", When = DateTime.UtcNow.AddDays(-1), IsVisible = true });
                _context.NewsItems.Add(new NewsItem { Title = "News Item 7", Text = "Today, even more stuff happened, but this time it took a lot more words to explain what exactly happened. This meant that the text was very long.", When = DateTime.UtcNow.AddHours(-1), IsVisible = true });
                _context.SaveChanges();
            }
        }

        // GET: api/News
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsItem>>> GetAll()
        {
            return await _context.NewsItems.OrderByDescending(o => o.When).ToListAsync();
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
            item.When = DateTime.UtcNow;
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