using System;

namespace PgccApi.Models.ViewModels
{
    public class EnquiryViewModel : ViewModelBase
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime When {get; set; }
    }
}