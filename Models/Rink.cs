namespace PgccApi.Models
{
    public class Rink : ModelBase
    {
        public Season Season { get; set; }
        public Competition Competition { get; set; }
        public string Skip { get; set; }
        public string Third { get; set; }
        public string Second { get; set; }
        public string Lead { get; set; }
        public bool WasWinningRink { get; set; }
    }
}