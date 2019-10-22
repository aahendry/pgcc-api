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
using AutoMapper;

namespace PgccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryController : ControllerBase
    {
        private readonly PgccContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IEnquiryService _enquiryService;

        public EnquiryController(PgccContext context, IMapper mapper, IEmailService emailService, IEnquiryService enquiryService)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
            _enquiryService = enquiryService;
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
        public async Task<ActionResult<Enquiry>> Post(EnquiryModel item)
        {
            var enquiry = _mapper.Map<Enquiry>(item); //new Enquiry() { Id = 0, Name = item.Name, Email = item.Email, Message = item.Message, When = DateTime.UtcNow };

            if (_enquiryService.Validate(item.RecaptchaToken))
            {
                _context.Enquiries.Add(enquiry);
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest();
            }

            try
            {
                _enquiryService.ProcessEnquiry(enquiry);
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