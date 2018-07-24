using System;
using System.Collections.Generic;
using System.Linq;
using ServicePlace.Model;

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
        public Order GetOrder(int id) => Orders.FirstOrDefault(x => x.Id == id);

        public void AddOrder(Order order)
        {
            order.Id = Orders.Last().Id + 1;
            Orders.Add(order);
        }

        public void RemoveOrder(int id) => Orders.Remove(Orders.FirstOrDefault(x => x.Id == id));
    }
}
