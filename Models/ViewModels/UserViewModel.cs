using System;

namespace PgccApi.Models.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiry { get; set; }
    }
}