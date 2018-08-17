using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ServicePlace.Model.DataModels;

namespace ServicePlace.Logic.Interfaces.Mappers
{
    public interface IOrderCategoryMapper
    {
        IEnumerable<SelectListItem> MapToSelectListItems(IEnumerable<OrderCategory> orderCategories);
    }
}
