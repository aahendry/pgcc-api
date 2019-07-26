namespace PgccApi.Models
{
    public class NewsItem : ModelBase
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsVisible { get; set; }
    }
}