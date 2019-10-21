using System.ComponentModel.DataAnnotations;

namespace PgccApi.Models
{
    public class EnquiryModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string RecaptchaToken { get; set; }
    }
}