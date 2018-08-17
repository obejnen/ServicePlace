using System.Collections.Generic;

namespace ServicePlace.Model.ViewModels.OrderCategoryViewModels
{
    public class IndexOrderCategoryViewModel
    {
        public IEnumerable<OrderCategoryViewModel> Categories { get; set; }
    }
}