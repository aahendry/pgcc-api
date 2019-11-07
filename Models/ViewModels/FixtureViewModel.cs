using PgccApi.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PgccApi.Models.ViewModels
{
    public class FixtureViewModel : ViewModelBase
    {
        public string Team1OtherName { get; set; }
        public string Team2OtherName { get; set; }

        public int? Shots1 { get; set; }
        public int? Shots2 { get; set; }

        public int? Ends1 { get; set; }
        public int? Ends2 { get; set; }

        public DateTime When { get; set; }
        public string Round { get; set; }
        public bool isFinal { get; set; }
        public string ManOfTheMatch { get; set; }


        public RinkViewModel Team1 { get; set; }
        public RinkViewModel Team2 { get; set; }
        public SeasonViewModel Season { get; set; }
        public CompetitionViewModel Competition { get; set; }
    }
}