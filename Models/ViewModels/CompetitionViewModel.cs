using System.ComponentModel.DataAnnotations.Schema;

namespace PgccApi.Models.ViewModels
{
    public class CompetitionViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public bool HasLeagueTable { get; set; }
        public string Blurb { get; set; }
    }
}