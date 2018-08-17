using System.Collections.Generic;
using System.Web.Mvc;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.ViewModels.OrderViewModels;

namespace ServicePlace.Logic.Interfaces.Mappers
{
    public interface IOrderMapper
    {
        OrderViewModel MapToOrderViewModel(Order order);

        IndexOrderViewModel MapToIndexOrderViewModel(IEnumerable<Order> orders, int[] pages);

        IndexOrderViewModel MapToIndexOrderViewModel(IEnumerable<Order> orders);

        IEnumerable<SelectListItem> MapToSelectListItems(IEnumerable<Order> orders);

        CreateOrderViewModel GetCreateViewModel();

        Order MapToOrderModel(CreateOrderViewModel createOrderViewModel, User creator);
    }
}
