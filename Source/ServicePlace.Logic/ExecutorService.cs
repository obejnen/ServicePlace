using System;
using System.Collections.Generic;
using System.Linq;
using ServicePlace.ViewModels;

namespace ServicePlace.Logic
{
    public class ExecutorService
    {
        public ExecutorService(List<Executor> executors)
        {
            Executors = executors;
        }

        public ExecutorService()
        {
            Executors = new List<Executor>();
        }

        public List<Executor> Executors { get; }
        public Executor GetExecutors(string id) => Executors.FirstOrDefault(x => x.Id == id);

        public void AddExecutor(Executor executor)
        {
            executor.Id = Guid.NewGuid().ToString();
            Executors.Add(executor);
        }

        public void RemoveExecutor(string id) => Executors.Remove(Executors.FirstOrDefault(x => x.Id == id));
    }
}
