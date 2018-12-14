using System;
using System.Collections.Generic;
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

        public override bool Equals(object obj)
        {
            var model = obj as ProviderResponseViewModel;
            return model != null &&
                   Id == model.Id &&
                   EqualityComparer<OrderViewModel>.Default.Equals(Order, model.Order) &&
                   EqualityComparer<ProviderViewModel>.Default.Equals(Provider, model.Provider) &&
                   Comment == model.Comment &&
                   CreatedAt == model.CreatedAt;
        }
    }
}