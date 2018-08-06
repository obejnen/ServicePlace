using System.Collections.Generic;
using System.Threading.Tasks;
using ServicePlace.Model;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IOrderRepository<T, TId, TResult> : IRepository<T, TId, TResult> where T : class
    {

        IEnumerable<Order> Search(string search);

        IEnumerable<Order> Take(int skip, int count);
    }
}
