using System;
using System.Collections.Generic;
using System.Linq;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.DataModels;

namespace ServicePlace.Logic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderResponseRepository _responseRepository;
        private readonly IOrderCategoryRepository _categoryRepository;

        public OrderService(IOrderRepository orderRepository,
            IOrderResponseRepository responseRepository,
            IOrderCategoryRepository categoryRepository)
        {
            _orderRepository = orderRepository;
            _responseRepository = responseRepository;
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Order> Orders => _orderRepository.GetAll();

        public void Create(Order order)
        {
            order.CreatedAt = DateTime.Now;
            order.UpdatedAt = DateTime.Now;
            _orderRepository.Create(order);
        }

        public void Delete(Order order)
        {
            _orderRepository.Delete(order);
        }

        public void Update(Order order)
        {
            order.UpdatedAt = DateTime.Now;
            _orderRepository.Update(order);
        }

        public void CloseOrder(int id)
        {
            _orderRepository.CloseOrder(id);
        }

        public void CompleteOrder(int orderId, int orderResponseId)
        {
            CloseOrder(orderId);
            var orderResponse = _responseRepository.GetBy(x => x.Id == orderResponseId).SingleOrDefault();
            if (orderResponse == null) return;
            orderResponse.Completed = true;
            _responseRepository.Update(orderResponse);
        }

        public Order Get(object id)
        {
            return _orderRepository.GetBy(x => x.Id == (int) id).SingleOrDefault();
        }

        public IEnumerable<Order> SearchOrder(string search)
        {
            return _orderRepository.GetBy(x => x.Title.Contains(search) || x.Body.Contains(search));
        }

        public IEnumerable<Order> Take(int skip, int count)
        {
            return _orderRepository.Take(skip, count);
        }

        public IEnumerable<Order> GetPage(int page, int perPage)
        {
            var ordersCount = _orderRepository.GetAll().Count();
            var skip = (page - 1) * perPage;
            return skip + perPage > ordersCount
                ? Take(skip, ordersCount % perPage)
                : Take(skip, perPage);
        }

        public IEnumerable<Order> GetPage(IEnumerable<Order> orders, int page, int perPage)
        {
            var ordersList = orders.ToList();
            var ordersCount = ordersList.Count();
            var skip = (page - 1) * perPage;
            return skip + perPage > ordersCount
                ? ordersList.Skip(skip).Take(ordersCount % perPage)
                : ordersList.Skip(skip).Take(perPage);
        }

        public int GetPagesCount(int perPage)
        {
            var ordersCount = _orderRepository.GetAll().Count();
            var count = ordersCount / perPage;
            return count * perPage == ordersCount ? count : count + 1;
        }

        public void CreateResponse(OrderResponse response)
        {
            response.Completed = false;
            response.CreatedAt = DateTime.Now;
            _responseRepository.Create(response);
        }

        public IEnumerable<OrderResponse> GetOrderResponses(int orderId)
        {
            return _responseRepository.GetBy(x => x.Order.Id == orderId);
        }

        //public Order GetOrderByProvider(int providerId, int orderId) => _responseRepository
        //    .GetBy(x => x.Order.Id == orderId && x.Provider.Id == providerId).SingleOrDefault()?.Order;

        public IEnumerable<Order> GetUserOrders(string userId) => _orderRepository.GetBy(x => x.Creator.Id == userId);

        public IEnumerable<OrderResponse> GetUserResponses(string userId) => _responseRepository.GetBy(x => x.Creator.Id == userId);

        public IEnumerable<OrderCategory> GetCategories()
        {
            return _categoryRepository.GetAll();
        }

        public IEnumerable<Order> GetProvidedOrders(string userId, int providerId)
        {
            return GetUserOrders(userId)
                .Select(x => _responseRepository
                                .GetBy(orderResponse =>
                                    orderResponse.Order.Id == orderResponse.Id
                                    && orderResponse.Provider.Id == providerId)
                                .SingleOrDefault()
                                ?.Order);
        }

        public OrderCategory GetCategory(int categoryId) => _categoryRepository.GetBy(x => x.Id == categoryId).SingleOrDefault();

        public IEnumerable<Order> GetByCategory(int categoryId) => _orderRepository.GetBy(x => x.Category.Id == categoryId);

        public IEnumerable<Order> GetProviderResponse(int providerId, IEnumerable<int> ordersId)
        {
            var responses = new List<Order>();

            foreach (var orderId in ordersId)
            {
                responses.AddRange(_responseRepository
                    .GetBy(x => x.Order.Id == orderId && x.Provider.Id == providerId)
                    .Select(x => x.Order));
            }

            return responses;
        }
    }
}