using System;
using System.Collections.Generic;
using ServicePlace.Model.LogicModels;
using ServicePlace.Model.ViewModels.AccountViewModels;

namespace ServicePlace.Model.ViewModels.OrderViewModels
{
    public class ShowViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Closed { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public CreatorViewModel Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}