using Microsoft.Extensions.Options;
using PgccApi.Models;
using PgccApi.Helpers;
using PgccApi.Entities;
using System.Net;
using Newtonsoft.Json;

namespace PgccApi.Services
{
    public interface IEnquiryService
    {
        void ProcessEnquiry(Enquiry enquiry);
        bool Validate(string encodedResponse);
    }

    public class EnquiryService : IEnquiryService
    {
        private readonly PgccContext _context;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public EnquiryService(PgccContext context, IOptions<AppSettings> appSettings, IEmailService emailService)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _emailService = emailService;
        }

        public void ProcessEnquiry(Enquiry enquiry)
        {
            if (_appSettings.SendEmailForEnquiries)
            {
                _emailService.SendEnquiryEmail(enquiry);
            }
        }

        public bool Validate(string encodedResponse)
        {
            if (!_appSettings.UseRecaptcha)
            {
                return true;
            }

            if (string.IsNullOrEmpty(encodedResponse)) return false;

            var secret = _appSettings.RecaptchaSecret;
            if (string.IsNullOrEmpty(secret)) return false;

            var client = new WebClient();

            var googleReply = client.DownloadString(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={encodedResponse}");

            return JsonConvert.DeserializeObject<RecaptchaResponse>(googleReply).Success;
        }
    }
}