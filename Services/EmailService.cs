using Microsoft.Extensions.Options;
using PgccApi.Models;
using PgccApi.Helpers;
using PgccApi.Entities;
using System.Net.Mail;
using System.Net;

namespace PgccApi.Services
{
    public interface IEmailService
    {
        void SendEnquiryEmail(Enquiry enquiry);
    }

    public class EmailService : IEmailService
    {
        private readonly PgccContext _context;
        private readonly AppSettings _appSettings;

        public EmailService(PgccContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public void SendEnquiryEmail(Enquiry enquiry)
        {
            if (_appSettings.SendEmailForEnquiries)
            {
                using (var message = new MailMessage())
                {
                    message.To.Add(new MailAddress(_appSettings.SmtpToAddress, _appSettings.SmtpToAlias));
                    message.From = new MailAddress(_appSettings.SmtpFromAddress, _appSettings.SmtpFromAlias);
                    message.Subject = "New PGCC Enquiry";
                    message.IsBodyHtml = false;
                    message.Body = "Hi,\r\n" +
                        "\r\n" +
                        $"Someone has sent a new enquiry from the PGCC website.\r\n" +
                        $"\r\n" +
                        $"Their email address is {enquiry.Email} .\r\n" +
                        $"Their name is {enquiry.Name} .\r\n" +
                        $"Their message is:\r\n" +
                        $"{enquiry.Message}\r\n" +
                        $"\r\n" +
                        $"Cheers,\r\n" +
                        $"portglasgowcurlingclub.co.uk";

                    using (var client = new SmtpClient(_appSettings.SmtpServer))
                    {
                        client.Port = _appSettings.SmtpPort;
                        client.EnableSsl = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(_appSettings.SmtpUsername, _appSettings.SmtpPassword);
                        client.Send(message);
                    }
                }
            }
        }
    }
}