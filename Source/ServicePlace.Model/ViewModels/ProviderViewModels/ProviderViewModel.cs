using System;
using System.Collections.Generic;
using ServicePlace.Model.ViewModels.AccountViewModels;

namespace ServicePlace.Model.ViewModels.ProviderViewModels
{
    public class ProviderViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public decimal? Price { get; set; }
        public IEnumerable<string> Images { get; set; }
        public UserViewModel User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}