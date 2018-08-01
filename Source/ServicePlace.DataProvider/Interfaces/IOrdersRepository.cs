using System.Collections.Generic;
using System.Threading.Tasks;
using ServicePlace.Model;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IOrdersRepository<T1, T2, TResult> : IRepository<T1, T2, TResult> where T1 : class
    {
        Task<Order> FindByNameAsync(string name);

        Task<IEnumerable<Order>> SearchByNameAsync(string name);

        Task<IEnumerable<Order>> Take(int count);
    }
}
