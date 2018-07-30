using System;
using System.Collections.Generic;
using System.Linq;
using ServicePlace.DataProvider.Repositories;
using ServicePlace.Model;

namespace ServicePlace.Logic
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService()
        {
            _orderRepository = new OrderRepository();
        }

        public IEnumerable<Order> Orders => _orderRepository.GetAllOrders();

        public void AddOrder(Order order)
        {
            order.CreatedAt = DateTime.Now;
            _orderRepository.AddOrder(order);
        }

        public Order GetOrder(int id) => _orderRepository.GetOrder(id);

        public void RemoveOrder(int id) => _orderRepository.RemoveOrder(id);

        public void UpdateOrder(Order order) => _orderRepository.UpdateOrder(order);
    }
}
