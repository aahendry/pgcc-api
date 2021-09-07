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
using PgccApi.Models.ViewModels;

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
        public async Task<ActionResult<IEnumerable<EnquiryViewModel>>> GetAll()
        {
            var query = _context.Enquiries.OrderByDescending(o => o.When);

            return await _mapper.ProjectTo<EnquiryViewModel>(query).ToListAsync();
        }

        // POST: api/Enquiry
        [HttpPost]
        public async Task<ActionResult<EnquiryViewModel>> Post(EnquiryModel item)
        {
            var enquiry = _mapper.Map<Enquiry>(item);

            if (_enquiryService.Validate(item.RecaptchaToken))
            {
                enquiry.When = DateTime.UtcNow;
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