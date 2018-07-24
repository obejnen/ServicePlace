using System;
using System.Collections.Generic;
using ServicePlace.Model;

namespace ServicePlace.Logic
{
    public static class OrderInitializer
    {
        private static OrderService _service;
        public static OrderService GetService(int count)
        {
            if (_service == null)
            {
                _service = new OrderService(new List<Order>());
                for (int i = 1; i <= count; i++)
                {
                    Order order = new Order
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = $"Order title #{i}",
                        Body = $"Order body #{i}"
                    };
                    _service.AddOrder(order);
                }
            }
            
            return _service;
        }
    }
}
