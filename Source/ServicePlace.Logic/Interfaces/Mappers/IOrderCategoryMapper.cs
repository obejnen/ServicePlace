using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.ViewModels.OrderCategoryViewModels;

namespace ServicePlace.Logic.Interfaces.Mappers
{
    public interface IOrderCategoryMapper
    {
        OrderCategoryViewModel MapToOrderCategoryViewModel(OrderCategory orderCategory);

        IEnumerable<SelectListItem> MapToSelectListItems(IEnumerable<OrderCategory> orderCategories);

        IndexOrderCategoryViewModel MapToIndexOrderCategoryViewModel(IEnumerable<OrderCategory> orderCategories);

        OrderCategory MapToOrderCategoryModel(CreateOrderCategoryViewModel viewModel);

        CreateOrderCategoryViewModel MapToCreateOrderCategoryViewModel(OrderCategory orderCategory);
    }
}
