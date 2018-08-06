using System.Collections.Generic;
using ServicePlace.Model;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IExecutorRepository<T, TId, TResult> : IRepository<T, TId, TResult> where T : class
    {
        IEnumerable<Executor> Search(string search);

        IEnumerable<Executor> Take(int skip, int count);
    }
}