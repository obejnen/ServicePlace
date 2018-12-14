using System.Collections.Generic;
using ServicePlace.Model.DTOModels;
using ServicePlace.Model.ViewModels.OrderCategoryViewModels;
using ServicePlace.Model.ViewModels.OrderResponseViewModels;
using ServicePlace.Model.ViewModels.OrderViewModels;
using ServicePlace.Model.ViewModels.ProviderCategoryViewModels;
using ServicePlace.Model.ViewModels.ProviderResponseViewModels;
using ServicePlace.Model.ViewModels.ProviderViewModels;

namespace ServicePlace.Model.ViewModels.AdminViewModels
{
    public class IndexAdminViewModel
    {
        public IEnumerable<UserDTO> Users { get; set; }
        public IndexOrderViewModel Orders { get; set; }
        public IndexProviderViewModel Providers { get; set; }
        public IndexOrderResponseViewModel OrderResponses { get; set; }
        public IndexProviderResponseViewModel ProviderResponses { get; set; }
        public IndexOrderCategoryViewModel OrderCategories { get; set; }
        public IndexProviderCategoryViewModel ProviderCategories { get; set; }

        public override bool Equals(object obj)
        {
            return obj is IndexAdminViewModel model &&
                   EqualityComparer<IEnumerable<UserDTO>>.Default.Equals(Users, model.Users) &&
                   EqualityComparer<IndexOrderViewModel>.Default.Equals(Orders, model.Orders) &&
                   EqualityComparer<IndexProviderViewModel>.Default.Equals(Providers, model.Providers) &&
                   EqualityComparer<IndexOrderResponseViewModel>.Default.Equals(OrderResponses, model.OrderResponses) &&
                   EqualityComparer<IndexProviderResponseViewModel>.Default.Equals(ProviderResponses, model.ProviderResponses) &&
                   EqualityComparer<IndexOrderCategoryViewModel>.Default.Equals(OrderCategories, model.OrderCategories) &&
                   EqualityComparer<IndexProviderCategoryViewModel>.Default.Equals(ProviderCategories, model.ProviderCategories);
        }
    }
}