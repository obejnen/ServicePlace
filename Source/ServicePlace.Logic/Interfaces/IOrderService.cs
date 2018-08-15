using System.Collections.Generic;
using ServicePlace.Model.LogicModels;

namespace ServicePlace.Logic.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> Orders { get; }

        void Create(Order order);

        void Delete(Order order);

        void Update(Order order);

        void Close(int id);

        void Complete(int orderId, int orderResponseId);

        Order FindById(object id);

        IEnumerable<Order> Search(string query);

        IEnumerable<Order> Take(int skip, int count);

        IEnumerable<Order> GetPage(int page, int perPage);

        int GetPagesCount(int perPage);

        void CreateResponse(OrderResponse response);

        IEnumerable<OrderResponse> GetOrderResponses(int orderId);

        Order GetOrderProvider(int providerId, int orderId);

        IEnumerable<Order> GetUserOrders(string userId);

        IEnumerable<OrderResponse> GetUserResponses(string userId);
    }
}