using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ServicePlace.Common.Enums;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Model;

namespace ServicePlace.Logic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository<Order, int, ResponseType> _orderRepository;

        public OrderService(IOrderRepository<Order, int, ResponseType> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IEnumerable<Order> Orders => _orderRepository.GetAll();

        public ResponseType Create(Order order)
        {
            order.CreatedAt = DateTime.Now;
            return _orderRepository.Create(order);
        }

        public ResponseType Delete(Order order)
        {
            return _orderRepository.Delete(order);
        }

        public ResponseType Update(Order order)
        {
            return _orderRepository.Update(order);
        }

        public Order FindById(int id)
        {
            return _orderRepository.FindById(id);
        }

        public IEnumerable<Order> Search(string search)
        {
            return _orderRepository.Search(search);
        }

        public IEnumerable<Order> Take(int skip, int count)
        {
            return _orderRepository.Take(skip, count);
        }
    }
}