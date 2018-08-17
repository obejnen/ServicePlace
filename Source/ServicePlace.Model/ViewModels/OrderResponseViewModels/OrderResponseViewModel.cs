using System;
using ServicePlace.Model.ViewModels.OrderViewModels;
using ServicePlace.Model.ViewModels.ProviderViewModels;

namespace ServicePlace.Model.ViewModels.OrderResponseViewModels
{
    public class OrderResponseViewModel
    {
        public int Id { get; set; }
        public ProviderViewModel Provider { get; set; }
        public OrderViewModel Order { get; set; }
        public decimal? Price { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Completed { get; set; }
    }
}
