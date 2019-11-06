using System.ComponentModel.DataAnnotations.Schema;

namespace PgccApi.Models.ViewModels
{
    public class RinkViewModel : ViewModelBase
    {
        public string Skip { get; set; }
        public string Third { get; set; }
        public string Second { get; set; }
        public string Lead { get; set; }
        public bool WasWinningRink { get; set; }

        public SeasonViewModel Season { get; set; }
        public CompetitionViewModel Competition { get; set; }
    }
}