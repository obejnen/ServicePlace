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
        private readonly IOrderResponseRepository _responseRepository;

        public OrderService(IOrderRepository orderRepository, IOrderResponseRepository responseRepository)
        {
            _orderRepository = orderRepository;
            _responseRepository = responseRepository;
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

        public IEnumerable<Order> GetPage(int page, int perPage)
        {
            int ordersCount = _orderRepository.GetOrdersCount();
            int skip = (page - 1) * perPage;
            return skip + perPage > ordersCount
                ? Take((page - 1) * perPage, ordersCount % perPage)
                : Take((page - 1) * perPage, perPage);
        }

        public int GetPagesCount(int perPage)
        {
            var ordersCount = _orderRepository.GetOrdersCount();
            int count = ordersCount / perPage;
            return count * perPage == ordersCount ? count : count + 1;
        }

        public void CreateResponse(OrderResponse response)
        {
            response.IsCompleted = false;
            response.CreatedAt = DateTime.Now;
            _responseRepository.Create(response);
        }

        public IEnumerable<OrderResponse> GetOrderResponses(int orderId)
        {
            return _responseRepository.GetOrderResponses(orderId);
        }

        public Order GetOrderProvider(int providerId, int orderId)
        {
            return _orderRepository.GetOrderProvider(providerId, orderId);
        }

        public IEnumerable<Order> GetUserOrders(string userId)
        {
            return _orderRepository.GetUserOrders(userId);
        }

        public IEnumerable<OrderResponse> GetUserResponses(string userId)
        {
            return _responseRepository.GetUserResponses(userId);
        }
    }
}