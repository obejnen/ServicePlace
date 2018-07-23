using System;
using System.Collections.Generic;
using System.Linq;
using ServicePlace.ViewModels;

namespace ServicePlace.Logic
{
    public class OrderService
    {
        public OrderService(List<Order> orders)
        {
            Orders = orders;
        }

        public OrderService()
        {
            Orders = new List<Order>();
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
