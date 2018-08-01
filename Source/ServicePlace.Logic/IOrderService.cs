using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ServicePlace.Common.Enums;
using ServicePlace.Model;

namespace ServicePlace.Logic
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> Orders { get; }

        Task<ResponseType> CreateAsync(Order order, CancellationToken cancellationToken);

        Task<ResponseType> DeleteAsync(Order order, CancellationToken cancellationToken);

        Task<ResponseType> UpdateAsync(Order order, CancellationToken cancellationToken);

        Task<Order> FindByIdAsync(int id);

        Task<IEnumerable<Order>> SearchAsync(string query);

        Task<IEnumerable<Order>> TakeAsync(int skip, int count);
    }
}