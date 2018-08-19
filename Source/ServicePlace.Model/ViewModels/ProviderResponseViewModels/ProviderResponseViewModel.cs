using System;
using ServicePlace.Model.ViewModels.OrderViewModels;
using ServicePlace.Model.ViewModels.ProviderViewModels;

namespace ServicePlace.Model.ViewModels.ProviderResponseViewModels
{
    public class ProviderResponseViewModel
    {
        public int Id { get; set; }
        public OrderViewModel Order { get; set; }
        public ProviderViewModel Provider { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}