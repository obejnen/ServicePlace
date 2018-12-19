using System;
using System.Collections.Generic;
using System.Linq;
using ServicePlace.Common;
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
        private readonly IContextProvider _contextProvider;

        public OrderService(IOrderRepository orderRepository,
            IOrderResponseRepository responseRepository,
            IOrderCategoryRepository categoryRepository,
            IContextProvider contextProvider
            )
        {
            _orderRepository = orderRepository;
            _responseRepository = responseRepository;
            _categoryRepository = categoryRepository;
            _contextProvider = contextProvider;
        }

        public IEnumerable<Order> Orders => _orderRepository.GetBy(x => x.Approved);

        public void Create(Order order)
        {
            order.CreatedAt = DateTime.Now;
            if (order.Images == null)
                order.Images = new[] { new Image { Url = Constants.DefaultOrderImage } };
            _orderRepository.Create(order);
            _contextProvider.CommitChanges();
        }

        public void Delete(Order order)
        {
            _orderRepository.Delete(order);
            _contextProvider.CommitChanges();
        }

        public void Update(Order order)
        {
            var orderToUpdate = _orderRepository.GetBy(x => x.Id == order.Id).SingleOrDefault();
            if (orderToUpdate == null) return;
            order.CreatedAt = orderToUpdate.CreatedAt;
            order.Images = orderToUpdate.Images;
            order.Approved = false;
            _orderRepository.Update(order);
            _contextProvider.CommitChanges();
        }

        public void CloseOrder(int id)
        {
            if (!_orderRepository.GetBy(x => x.Id == id).Any()) return;
            _orderRepository.CloseOrder(id);
            _contextProvider.CommitChanges();
        }

        public void CompleteOrder(int orderId, int orderResponseId)
        {
            var orderResponse = _responseRepository.GetBy(x => x.Id == orderResponseId).SingleOrDefault();
            if (orderResponse == null || orderResponse.Order.Id != orderId) return;
            CloseOrder(orderId);
            orderResponse.Completed = true;
            _responseRepository.Update(orderResponse);
            _contextProvider.CommitChanges();
        }

        public void ApproveOrder(int orderId)
        {
            var order = _orderRepository.GetBy(x => x.Id == orderId).SingleOrDefault();
            if (order == null) return;
            order.Approved = true;
            _orderRepository.Update(order);
            _contextProvider.CommitChanges();
        }

        public Order Get(object id)
        {
            return _orderRepository.GetBy(x => x.Id == (int) id).SingleOrDefault();
        }

        public IEnumerable<Order> GetAll() => _orderRepository.GetAll().OrderBy(x => x.CreatedAt);

        public IEnumerable<OrderResponse> GetAllOrderResponses() => _responseRepository.GetAll();

        public IEnumerable<Order> SearchOrder(string search, int categoryId)
        {
            return _orderRepository.GetBy(x => (x.Title.Contains(search) || x.Body.Contains(search)) && x.Approved && !x.Closed
                                               && (categoryId <= 0 || x.Category.Id == categoryId));
        }

        public IEnumerable<Order> Take(int skip, int count)
        {
            return Orders.Skip(skip).Take(count);
        }

        public IEnumerable<Order> GetPage(int page, int perPage)
        {
            var ordersCount = Orders.Count();
            var skip = (page - 1) * perPage;
            return skip + perPage > ordersCount
                ? Take(skip, ordersCount % perPage)
                : Take(skip, perPage);
        }

        public IEnumerable<Order> GetPage(IEnumerable<Order> orders, int page, int perPage)
        {
            var ordersList = orders.ToList();
            var ordersCount = ordersList.Count;
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
            _contextProvider.CommitChanges();
        }

        public void DeleteResponse(OrderResponse response)
        {
            _responseRepository.Delete(response);
            _contextProvider.CommitChanges();
        }

        public IEnumerable<OrderResponse> GetOrderResponses(int orderId)
        {
            return _responseRepository.GetBy(x => x.Order.Id == orderId);
        }

        public IEnumerable<Order> GetUserOrders(string userId) => _orderRepository.GetBy(x => x.Creator.Id == userId);

        public IEnumerable<OrderResponse> GetUserResponses(string userId) => _responseRepository.GetBy(x => x.Creator.Id == userId);

        public IEnumerable<OrderCategory> GetCategories()
        {
            return _categoryRepository.GetAll();
        }

        public IEnumerable<Order> GetProvidedOrders(string userId, int providerId)
        {
            return GetUserOrders(userId).ToList()
                .Where(x => _responseRepository
                                .GetBy(orderResponse =>
                                    orderResponse.Order.Id == x.Id
                                    && orderResponse.Provider.Id == providerId)
                                .SingleOrDefault() != null);
        }

        public OrderCategory GetCategory(int categoryId) => _categoryRepository.GetBy(x => x.Id == categoryId).SingleOrDefault();

        public IEnumerable<Order> GetByCategory(int categoryId) => _orderRepository.GetBy(x => x.Category.Id == categoryId && x.Approved);

        public void CreateCategory(OrderCategory orderCategory)
        {
            if (_categoryRepository.GetBy(x => x.Name == orderCategory.Name).Any()) return;
            _categoryRepository.Create(orderCategory);
            _contextProvider.CommitChanges();
        }
    }
}