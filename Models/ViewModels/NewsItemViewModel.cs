using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PgccApi.Models.ViewModels
{
    public class NewsItemViewModel : ViewModelBase
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime When {get; set; }
        public bool IsVisible { get; set; }
    }
}