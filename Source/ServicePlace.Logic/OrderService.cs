using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ServicePlace.Common.Enums;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Model;

namespace ServicePlace.Logic
{
    public class OrderService : IOrderService
    {
        private readonly IOrdersRepository<Order, int, ResponseType> _ordersRepository;

        public OrderService(IOrdersRepository<Order, int, ResponseType> ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public Task<IEnumerable<Order>> Orders => _ordersRepository.GetAll();

        public Task<ResponseType> CreateAsync(Order order, CancellationToken cancellationToken)
        {
            order.CreatedAt = DateTime.Now;
            return _ordersRepository.CreateAsync(order, cancellationToken);
        }

        public Task<ResponseType> DeleteAsync(Order order, CancellationToken cancellationToken)
        {
            return _ordersRepository.DeleteAsync(order, cancellationToken);
        }

        public Task<ResponseType> UpdateAsync(Order order, CancellationToken cancellationToken)
        {
            return _ordersRepository.UpdateAsync(order, cancellationToken);
        }

        public Task<Order> FindByIdAsync(int id)
        {
            return _ordersRepository.FindByIdAsync(id);
        }

        public Task<IEnumerable<Order>> SearchAsync(string search)
        {
            return _ordersRepository.SearchAsync(search);
        }

        public Task<IEnumerable<Order>> TakeAsync(int skip, int count)
        {
            return _ordersRepository.Take(skip, count);
        }
    }
}
