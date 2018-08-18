using ServicePlace.Model.ViewModels.OrderResponseViewModels;
using ServicePlace.Model.ViewModels.OrderViewModels;
using ServicePlace.Model.ViewModels.ProviderResponseViewModels;
using ServicePlace.Model.ViewModels.ProviderViewModels;

namespace ServicePlace.Model.ViewModels
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
    }
}