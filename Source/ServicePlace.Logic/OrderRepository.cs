using System;
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
        public Order GetOrder(string id) => Orders.FirstOrDefault(x => x.Id == id);

        public void AddOrder(Order order)
        {
            order.Id = Guid.NewGuid().ToString();
            Orders.Add(order);
        }

        public void RemoveOrder(string id) => Orders.Remove(Orders.FirstOrDefault(x => x.Id == id));
    }
}
