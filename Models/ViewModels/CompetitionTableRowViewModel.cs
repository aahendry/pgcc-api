namespace PgccApi.Models.ViewModels
{
    public class CompetitionTableRowViewModel
    {
        public RinkViewModel Rink { get; set; }
        public int Played { get; set; }
        public int Won { get; set; }
        public int Drawn { get; set; }
        public int Lost { get; set; }
        public int For { get; set; }
        public int Against { get; set; }
        public int Shots { get; set; }
        public int EndsWon { get; set; }
        public int Points { get; set; }
    }
}