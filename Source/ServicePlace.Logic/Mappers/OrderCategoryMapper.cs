using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Model.DataModels;

namespace ServicePlace.Logic.Mappers
{
    public class OrderCategoryMapper : IOrderCategoryMapper
    {
        public IEnumerable<SelectListItem> MapToSelectListItems(IEnumerable<OrderCategory> orderCategories)
        {
            return orderCategories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
        }
    }
}
