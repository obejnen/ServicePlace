using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicePlace.Models;

namespace ServicePlace.Logic
{
    public static class OrderSeeder
    {
        public static OrderRepository GetRepository(int count)
        {
            var repository = new OrderRepository(new List<Order>());
            for (int i = 1; i <= count; i++)
            {
                Order order = new Order
                {
                    Id = i,
                    Title = $"Order title #{i}",
                    Body = $"Order body #{i}"
                };
                repository.AddOrder(order);
            }

            return repository;
        }
    }
}
