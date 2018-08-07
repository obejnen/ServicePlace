using System.Collections.Generic;
using ServicePlace.Model.LogicModels;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IOrderResponseRepository : IRepository<OrderResponse>
    {
        IEnumerable<OrderResponse> GetOrderResponses(int orderId);
    }
}
