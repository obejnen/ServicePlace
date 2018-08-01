using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IRepository<T, TId, TResult> where T : class
    {
        Task<TResult> CreateAsync(T model, CancellationToken cancellationToken);

        Task<TResult> DeleteAsync(T model, CancellationToken cancellationToken);

        Task<TResult> UpdateAsync(T model, CancellationToken cancellationToken);

        Task<T> FindByIdAsync(TId id);

        Task<IEnumerable<T>> GetAll();

        void Dispose();
    }
}