using System;
using System.Collections.Generic;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.ViewModels.AccountViewModels;

namespace ServicePlace.Model.ViewModels.OrderViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Closed { get; set; }
        public IEnumerable<string> Images { get; set; }
        public UserViewModel User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}