using System.Collections.Generic;

namespace ServicePlace.Model.ViewModels.OrderCategoryViewModels
{
    public class IndexOrderCategoryViewModel
    {
        public int SelectedCategoryId { get; set; }
        public IEnumerable<OrderCategoryViewModel> Categories { get; set; }

        public override bool Equals(object obj)
        {
            return obj is IndexOrderCategoryViewModel model &&
                   SelectedCategoryId == model.SelectedCategoryId &&
                   EqualityComparer<IEnumerable<OrderCategoryViewModel>>.Default.Equals(Categories, model.Categories);
        }
    }
}