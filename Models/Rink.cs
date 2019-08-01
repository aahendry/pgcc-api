using System;

namespace PgccApi.Models
{
    public class Rink : ModelBase
    {
        public string Season { get; set; }
        public string Competition { get; set; }
        public string Skip { get; set; }
        public string Third { get; set; }
        public string Second { get; set; }
        public string Lead { get; set; }
    }
}