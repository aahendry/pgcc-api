using System.ComponentModel.DataAnnotations.Schema;

namespace PgccApi.Entities
{
    public class Competition : EntityBase
    {
        public string Name { get; set; }
        public bool HasLeagueTable { get; set; }
        public string Blurb { get; set; }
    }
}