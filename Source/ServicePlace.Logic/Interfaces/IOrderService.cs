using System.Collections.Generic;
using ServicePlace.Model.DataModels;

namespace ServicePlace.Logic.Interfaces
{
    public interface IOrderService : IService<Order>
    {
        IEnumerable<Order> Orders { get; }

        void CloseOrder(int id);

        void CompleteOrder(int orderId, int orderResponseId);

        IEnumerable<Order> SearchOrder(string query);

        IEnumerable<Order> Take(int skip, int count);

        IEnumerable<Order> GetPage(int page, int perPage);

        int GetPagesCount(int perPage);

        void CreateResponse(OrderResponse response);

        IEnumerable<OrderResponse> GetOrderResponses(int orderId);

        Order GetOrderProvider(int providerId, int orderId);

        IEnumerable<Order> GetUserOrders(string userId);

        IEnumerable<OrderResponse> GetUserResponses(string userId);

        IEnumerable<OrderCategory> GetCategories();

        OrderCategory GetCategory(int id);

        IEnumerable<Order> GetByCategory(int id);
    }
}