using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using PgccApi.Models;
using PgccApi.Entities;
using PgccApi.Services;

namespace PgccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryController : ControllerBase
    {
        private readonly PgccContext _context;
        private readonly IEmailService _emailService;

        public EnquiryController(PgccContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: api/Enquiry
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enquiry>>> GetAll()
        {
            return await _context.Enquiries.OrderByDescending(o => o.When).ToListAsync();
        }

        // POST: api/Enquiry
        [HttpPost]
        public async Task<ActionResult<Enquiry>> Post(Enquiry item)
        {
            item.When = DateTime.UtcNow;
            _context.Enquiries.Add(item);
            await _context.SaveChangesAsync();

            try
            {
                _emailService.SendEnquiryEmail(item);
            }
            catch
            {
                // Service unavailable
                return StatusCode(503);
            }

            return Ok();
        }

        // DELETE: api/Enquiry/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var item = await _context.Enquiries.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Enquiries.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}