using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.ViewModels.OrderCategoryViewModels;

namespace ServicePlace.Logic.Mappers
{
    public class OrderCategoryMapper : IOrderCategoryMapper
    {
        public OrderCategoryViewModel MapToOrderCategoryViewModel(OrderCategory orderCategory)
        {
            return new OrderCategoryViewModel
            {
                Id = orderCategory.Id,
                Name = orderCategory.Name
            };
        }

        public IEnumerable<SelectListItem> MapToSelectListItems(IEnumerable<OrderCategory> orderCategories)
        {
            return orderCategories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
        }

        public IndexOrderCategoryViewModel MapToIndexOrderCategoryViewModel(IEnumerable<OrderCategory> orderCategories)
        {
            return new IndexOrderCategoryViewModel
            {
                Categories = orderCategories.Select(MapToOrderCategoryViewModel)
            };
        }

        public OrderCategory MapToOrderCategoryModel(CreateOrderCategoryViewModel createViewModel)
        {
            return new OrderCategory
            {
                Name = createViewModel.Name
            };
        }
    }
}
