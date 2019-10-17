using System;

namespace PgccApi.Entities
{
    public class Enquiry : EntityBase
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime When {get; set; }
    }
}