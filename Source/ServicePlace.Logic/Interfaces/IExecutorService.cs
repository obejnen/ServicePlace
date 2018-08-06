using System.Collections.Generic;
using ServicePlace.Common.Enums;
using ServicePlace.Model;

namespace ServicePlace.Logic.Interfaces
{
    public interface IExecutorService
    {
        IEnumerable<Executor> Executors { get; }

        ResponseType Create(Executor executor);

        ResponseType Delete(Executor order);

        ResponseType Update(Executor executor);

        Executor FindById(int id);

        IEnumerable<Executor> Search(string query);

        IEnumerable<Executor> Take(int skip, int count);
    }
}