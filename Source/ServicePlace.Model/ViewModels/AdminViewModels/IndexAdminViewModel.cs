using System.Collections.Generic;
using ServicePlace.Model.DTOModels;
using ServicePlace.Model.ViewModels.OrderResponseViewModels;
using ServicePlace.Model.ViewModels.OrderViewModels;
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
    }
}