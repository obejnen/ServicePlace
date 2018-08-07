using System;
using System.Collections.Generic;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Model.LogicModels;

namespace ServicePlace.Logic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderResponseRepository _orderResponseRepository;

        public OrderService(IOrderRepository orderRepository, IOrderResponseRepository orderResponseRepository)
        {
            _orderRepository = orderRepository;
            _orderResponseRepository = orderResponseRepository;
        }

        public IEnumerable<Order> Orders => _orderRepository.GetAll();

        public void Create(Order order)
        {
            order.CreatedAt = DateTime.Now;
            _orderRepository.Create(order);
        }

        public void Delete(Order order)
        {
            _orderRepository.Delete(order);
        }

        public void Update(Order order)
        {
            _orderRepository.Update(order);
        }

        public Order FindById(object id)
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

        public void CreateResponse(OrderResponse response)
        {
            response.IsCompleted = false;
            response.CreatedAt = DateTime.Now;
            _orderResponseRepository.Create(response);
        }

        public IEnumerable<OrderResponse> GetOrderResponses(int orderId)
        {
            return _orderResponseRepository.GetOrderResponses(orderId);
        }
    }
}