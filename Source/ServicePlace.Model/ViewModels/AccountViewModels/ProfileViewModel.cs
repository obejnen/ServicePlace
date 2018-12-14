using ServicePlace.Model.ViewModels.OrderResponseViewModels;
using ServicePlace.Model.ViewModels.OrderViewModels;
using ServicePlace.Model.ViewModels.ProviderResponseViewModels;
using ServicePlace.Model.ViewModels.ProviderViewModels;
using System.Collections.Generic;

namespace ServicePlace.Model.ViewModels.AccountViewModels
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        public string Avatar { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public IndexOrderViewModel Orders { get; set; }
        public IndexProviderViewModel Providers { get; set; }
        public IndexOrderResponseViewModel OrderResponses { get; set; }
        public IndexProviderResponseViewModel ProviderResponses { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ProfileViewModel model &&
                   Id == model.Id &&
                   Avatar == model.Avatar &&
                   UserName == model.UserName &&
                   Name == model.Name &&
                   EqualityComparer<IndexOrderViewModel>.Default.Equals(Orders, model.Orders) &&
                   EqualityComparer<IndexProviderViewModel>.Default.Equals(Providers, model.Providers) &&
                   EqualityComparer<IndexOrderResponseViewModel>.Default.Equals(OrderResponses, model.OrderResponses) &&
                   EqualityComparer<IndexProviderResponseViewModel>.Default.Equals(ProviderResponses, model.ProviderResponses);
        }
    }
}