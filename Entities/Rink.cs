using System.ComponentModel.DataAnnotations.Schema;

namespace PgccApi.Entities
{
    public class Rink : EntityBase
    {
        public Season Season { get; set; }
        public Competition Competition { get; set; }
        public string Skip { get; set; }
        public string Third { get; set; }
        public string Second { get; set; }
        public string Lead { get; set; }
        [Column(TypeName = "bit")]
        public bool WasWinningRink { get; set; }
    }
}