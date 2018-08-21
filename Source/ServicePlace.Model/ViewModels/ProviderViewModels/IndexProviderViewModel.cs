using System.Collections.Generic;

namespace ServicePlace.Model.ViewModels.ProviderViewModels
{
    public class IndexProviderViewModel
    {
        public int CurrentPage { get; set; }
        public int MaxPage { get; set; }
        public int MinPage { get; set; }
        public IEnumerable<ProviderViewModel> FirstPart { get; set; }
        public IEnumerable<ProviderViewModel> SecondPart { get; set; }
        public IEnumerable<ProviderViewModel> Providers { get; set; }
    }
}