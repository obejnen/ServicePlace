using ServicePlace.Models;
using System.Collections.Generic;

namespace ServicePlace.Logic
{
    public static class ExecutorSeeder
    {
        public static ExecutorRepository GetRepository(int count)
        {
            var repository = new ExecutorRepository(new List<Executor>());
            
            for(int i = 1; i <= count; i++)
            {
                var executor = new Executor
                {
                    Id = i,
                    Title = $"Executor title #{i}",
                    Body = $"Executor body #{i}"
                };
                repository.AddExecutor(executor);
            }

            return repository;
        }
    }
}