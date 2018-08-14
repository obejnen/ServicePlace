using System.Collections.Generic;

namespace ServicePlace.Model.ViewModels
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public IEnumerable<OrderViewModels.IndexViewModel> Orders { get; set; }
        public IEnumerable<ProviderViewModels.IndexViewModel> Providers { get; set; }
        public IEnumerable<OrderResponseViewModels.IndexViewModel> OrderResponses { get; set; }
        public IEnumerable<ProviderResponseViewModels.IndexViewModel> ProviderResponses { get; set; }
    }
}