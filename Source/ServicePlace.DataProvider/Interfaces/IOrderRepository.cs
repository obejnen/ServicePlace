using System.Collections.Generic;
using ServicePlace.Model;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {

        IEnumerable<Order> Search(string search);

        IEnumerable<Order> Take(int skip, int count);
    }
}
