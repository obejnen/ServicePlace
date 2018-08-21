using System.Collections.Generic;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.ViewModels.OrderResponseViewModels;

namespace ServicePlace.Logic.Interfaces.Mappers
{
    public interface IOrderResponseMapper
    {
        OrderResponseViewModel MapToOrderResponseViewModel(OrderResponse orderResponse);

        IndexOrderResponseViewModel MapToIndexOrderResponseViewModel(IEnumerable<OrderResponse> orderResponses);

        CreateOrderResponseViewModel GetCreateOrderResponseViewModel(string userId, int orderId);

        OrderResponse MapToOrderResponseModel(CreateOrderResponseViewModel createViewModel, User creator);
    }
}
