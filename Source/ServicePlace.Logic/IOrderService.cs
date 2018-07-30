using System.Collections.Generic;
using ServicePlace.Model;

namespace ServicePlace.Logic
{
    public interface IOrderService
    {
        IEnumerable<Order> Orders { get; }
        void AddOrder(Order order);
        Order GetOrder(int id);
        void RemoveOrder(int id);
        void UpdateOrder(Order order);
    }
}