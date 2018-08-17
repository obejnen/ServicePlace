using System.Collections.Generic;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        void CloseOrder(int id);

        IEnumerable<Order> Take(int skip, int count);
    }
}
