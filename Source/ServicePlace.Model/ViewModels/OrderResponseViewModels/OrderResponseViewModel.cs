using System;
using System.Collections.Generic;
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

        public override bool Equals(object obj)
        {
            return obj is OrderResponseViewModel model &&
                   Id == model.Id &&
                   EqualityComparer<ProviderViewModel>.Default.Equals(Provider, model.Provider) &&
                   EqualityComparer<OrderViewModel>.Default.Equals(Order, model.Order) &&
                   EqualityComparer<decimal?>.Default.Equals(Price, model.Price) &&
                   Comment == model.Comment &&
                   CreatedAt == model.CreatedAt &&
                   Completed == model.Completed;
        }
    }
}
