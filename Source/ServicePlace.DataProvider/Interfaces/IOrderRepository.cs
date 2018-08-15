using System.Collections.Generic;
using ServicePlace.Model.LogicModels;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        int GetOrdersCount();

        void Close(int id);

        IEnumerable<Order> Search(string search);

        IEnumerable<Order> Take(int skip, int count);

        Order GetOrderProvider(int providerId, int orderId);

        IEnumerable<Order> GetUserOrders(string userId);
    }
}
