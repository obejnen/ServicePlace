using System.Collections.Generic;

namespace ServicePlace.Model.ViewModels.ProviderCategoryViewModels
{
    public class IndexProviderCategoryViewModel
    {
        public int SelectedCategoryId { get; set; }
        public IEnumerable<ProviderCategoryViewModel> Categories { get; set; }
    }
}
