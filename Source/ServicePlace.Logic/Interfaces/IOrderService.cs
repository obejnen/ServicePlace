using System.Collections.Generic;
using ServicePlace.Common.Enums;
using ServicePlace.Model;

namespace ServicePlace.Logic.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> Orders { get; }

        ResponseType Create(Order order);

        ResponseType Delete(Order order);

        ResponseType Update(Order order);

        Order FindById(int id);

        IEnumerable<Order> Search(string query);

        IEnumerable<Order> Take(int skip, int count);
    }
}