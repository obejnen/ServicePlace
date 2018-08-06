using System;
using ServicePlace.Model.ViewModels.AccountViewModels;

namespace ServicePlace.Website.Models.ExecutorViewModels
{
    public class ShowViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public decimal? Price { get; set; }
        public CreatorViewModel Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}