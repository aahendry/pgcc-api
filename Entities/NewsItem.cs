using System;

namespace PgccApi.Entities
{
    public class NewsItem : EntityBase
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime When {get; set; }
        public bool IsVisible { get; set; }
    }
}