using System;
using System.Collections.Generic;
using ServicePlace.Models;

namespace ServicePlace.Logic
{
    public static class OrderSeeder
    {
        private static OrderRepository _repository;
        public static OrderRepository GetRepository(int count)
        {
            if (_repository == null)
            {
                _repository = new OrderRepository(new List<Order>());
                for (int i = 1; i <= count; i++)
                {
                    Order order = new Order
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = $"Order title #{i}",
                        Body = $"Order body #{i}"
                    };
                    _repository.AddOrder(order);
                }
            }
            
            return _repository;
        }
    }
}
