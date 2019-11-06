using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PgccApi.Entities
{
    public class Fixture : EntityBase
    {
        public long SeasonId { get; set; }
        public long CompetitionId { get; set; }
        public long? Team1Id { get; set; }
        public long? Team2Id { get; set; }

        public string Team1OtherName { get; set; }
        public string Team2OtherName { get; set; }

        public int? Shots1 { get; set; }
        public int? Shots2 { get; set; }

        public int? Ends1 { get; set; }
        public int? Ends2 { get; set; }

        public DateTime When { get; set; }
        public string Round { get; set; }
        //[Column(TypeName = "bit")]
        public bool IsFinal { get; set; }


        public Rink Team1 { get; set; }
        public Rink Team2 { get; set; }
        public Season Season { get; set; }
        public Competition Competition { get; set; }
    }
}