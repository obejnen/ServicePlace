using System.Collections.Generic;

namespace ServicePlace.Model.ViewModels.OrderCategoryViewModels
{
    public class IndexOrderCategoryViewModel
    {
        public int SelectedCategoryId { get; set; }
        public IEnumerable<OrderCategoryViewModel> Categories { get; set; }
    }
}