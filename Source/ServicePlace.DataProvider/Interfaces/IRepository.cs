using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IRepository<T, TId, TResult> where T : class
    {
        TResult Create(T model);

        TResult Delete(T model);

        TResult Update(T model);

        T FindById(TId id);

        IEnumerable<T> GetAll();

        void Dispose();
    }
}