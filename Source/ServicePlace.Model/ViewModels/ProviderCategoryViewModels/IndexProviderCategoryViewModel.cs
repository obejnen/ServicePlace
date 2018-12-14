using System.Collections.Generic;

namespace ServicePlace.Model.ViewModels.ProviderCategoryViewModels
{
    public class IndexProviderCategoryViewModel
    {
        public int SelectedCategoryId { get; set; }
        public IEnumerable<ProviderCategoryViewModel> Categories { get; set; }

        public override bool Equals(object obj)
        {
            var model = obj as IndexProviderCategoryViewModel;
            return model != null &&
                   SelectedCategoryId == model.SelectedCategoryId &&
                   EqualityComparer<IEnumerable<ProviderCategoryViewModel>>.Default.Equals(Categories, model.Categories);
        }
    }
}
