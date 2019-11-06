using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PgccApi.Entities
{
    public class NewsItem : EntityBase
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime When {get; set; }
        //[Column(TypeName = "bit")]
        public bool IsVisible { get; set; }
    }
}