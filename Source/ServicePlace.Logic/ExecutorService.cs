using System;
using System.Collections.Generic;
using System.Linq;
using ServicePlace.Model;

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
        public Executor GetExecutors(int id) => Executors.FirstOrDefault(x => x.Id == id);

        public void AddExecutor(Executor executor)
        {
            Executors.Add(executor);
        }

        public void RemoveExecutor(int id) => Executors.Remove(Executors.FirstOrDefault(x => x.Id == id));
    }
}
