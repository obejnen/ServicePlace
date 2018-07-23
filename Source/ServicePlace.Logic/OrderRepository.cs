using System.Collections.Generic;
using System.Linq;
using ServicePlace.Models;

namespace ServicePlace.Logic
{
    public class OrderRepository
    {
        public OrderRepository(List<Order> orders)
        {
            Orders = orders;
        }

        public List<Order> Orders { get; }
        public Order GetOrder(int id) => Orders.FirstOrDefault(x => x.Id == id);

        public void AddOrder(Order order) => Orders.Add(order);

        public void RemoveOrder(int id) => Orders.Remove(Orders.FirstOrDefault(x => x.Id == id));
    }
}
