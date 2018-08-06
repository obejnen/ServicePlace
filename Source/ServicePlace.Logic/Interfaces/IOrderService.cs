using System.Collections.Generic;
using ServicePlace.Model;

namespace ServicePlace.Logic.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> Orders { get; }

        void Create(Order order);

        void Delete(Order order);

        void Update(Order order);

        Order FindById(object id);

        IEnumerable<Order> Search(string query);

        IEnumerable<Order> Take(int skip, int count);
    }
}