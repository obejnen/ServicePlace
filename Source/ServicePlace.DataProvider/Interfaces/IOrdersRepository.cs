using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServicePlace.Model;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IOrdersRepository<T, TId, TResult> : IRepository<T, TId, TResult> where T : class
    {

        Task<IEnumerable<Order>> SearchAsync(string search);

        Task<IEnumerable<Order>> Take(int skip, int count);
    }
}
