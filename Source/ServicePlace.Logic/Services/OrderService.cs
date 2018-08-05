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

        public Task<IEnumerable<Order>> Orders => _orderRepository.GetAll();

        public Task<ResponseType> CreateAsync(Order order, CancellationToken cancellationToken)
        {
            order.CreatedAt = DateTime.Now;
            return _orderRepository.CreateAsync(order, cancellationToken);
        }

        public Task<ResponseType> DeleteAsync(Order order, CancellationToken cancellationToken)
        {
            return _orderRepository.DeleteAsync(order, cancellationToken);
        }

        public Task<ResponseType> UpdateAsync(Order order, CancellationToken cancellationToken)
        {
            return _orderRepository.UpdateAsync(order, cancellationToken);
        }

        public Task<Order> FindByIdAsync(int id)
        {
            return _orderRepository.FindByIdAsync(id);
        }

        public Task<IEnumerable<Order>> SearchAsync(string search)
        {
            return _orderRepository.SearchAsync(search);
        }

        public Task<IEnumerable<Order>> TakeAsync(int skip, int count)
        {
            return _orderRepository.Take(skip, count);
        }
    }
}