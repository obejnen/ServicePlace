using System.Collections.Generic;
using System.Linq;
using ServicePlace.Models;

namespace ServicePlace.Logic
{
    public class ExecutorRepository
    {
        public ExecutorRepository(List<Executor> executors)
        {
            Executors = executors;
        }

        public List<Executor> Executors { get; }
        public Executor GetExecutors(int id) => Executors.FirstOrDefault(x => x.Id == id);

        public void AddExecutor(Executor executor) => Executors.Add(executor);

        public void RemoveExecutor(int id) => Executors.Remove(Executors.FirstOrDefault(x => x.Id == id));
    }
}
