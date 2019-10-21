namespace PgccApi.Helpers
{
    public class AppSettings
    {
        public string TokenSecret { get; set; }

        public bool SendEmailForEnquiries { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpFromAddress { get; set; }
        public string SmtpFromAlias { get; set; }
        public string SmtpToAddress { get; set; }
        public string SmtpToAlias { get; set; }

        public bool UseRecaptcha { get; set; }
        public string RecaptchaSecret { get; set; }
    }
}