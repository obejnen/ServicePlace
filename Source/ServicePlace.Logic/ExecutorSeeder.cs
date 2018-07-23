using System;
using ServicePlace.Models;
using System.Collections.Generic;

namespace ServicePlace.Logic
{
    public static class ExecutorSeeder
    {
        private static ExecutorRepository _repository;
        public static ExecutorRepository GetRepository(int count)
        {
            if (_repository == null)
            {
                _repository = new ExecutorRepository(new List<Executor>());

                for (int i = 1; i <= count; i++)
                {
                    var executor = new Executor
                    {
                        Id = new Guid().ToString(),
                        Title = $"Executor title #{i}",
                        Body = $"Executor body #{i}"
                    };
                    _repository.AddExecutor(executor);
                }
            }

            return _repository;
        }
    }
}