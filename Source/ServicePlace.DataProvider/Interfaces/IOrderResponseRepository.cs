using System.Collections.Generic;
using ServicePlace.Model.LogicModels;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IOrderResponseRepository : IRepository<OrderResponse>
    {
        void Complete(int orderResponseId);

        IEnumerable<OrderResponse> GetOrderResponses(int orderId);

        IEnumerable<OrderResponse> GetUserResponses(string userId);
    }
}
