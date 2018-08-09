using System.Collections.Generic;
using ServicePlace.Model.LogicModels;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        int GetOrdersCount();

        IEnumerable<Order> Search(string search);

        IEnumerable<Order> Take(int skip, int count);

        Order GetOrderProvider(int providerId, int orderId);
    }
}
