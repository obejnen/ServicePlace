using System.Collections.Generic;
using ServicePlace.Common.Enums;
using ServicePlace.Model;

namespace ServicePlace.Logic.Interfaces
{
    public interface IExecutorService
    {
        IEnumerable<Executor> Executors { get; }

        void Create(Executor executor);

        void Delete(Executor executor);

        void Update(Executor executor);

        Executor FindById(object id);

        IEnumerable<Executor> Search(string query);

        IEnumerable<Executor> Take(int skip, int count);
    }
}