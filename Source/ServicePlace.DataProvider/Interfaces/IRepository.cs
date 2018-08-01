using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IRepository<T1, T2, TResult> where T1 : class
    {
        Task<TResult> CreateAsync(T1 model, CancellationToken cancellationToken);

        Task<TResult> DeleteAsync(T1 model, CancellationToken cancellationToken);

        Task<TResult> UpdateAsync(T1 model, CancellationToken cancellationToken);

        Task<T1> FindByIdAsync(T2 id);

        Task<IEnumerable<T1>> GetAll();

        void Dispose();
    }
}