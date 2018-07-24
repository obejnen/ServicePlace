using System;
using ServicePlace.Model;
using System.Collections.Generic;

namespace ServicePlace.Logic
{
    public static class ExecutorInitializer
    {
        private static ExecutorService _service;
        public static ExecutorService GetService(int count)
        {
            if (_service == null)
            {
                _service = new ExecutorService(new List<Executor>());

                for (int i = 1; i <= count; i++)
                {
                    var executor = new Executor
                    {
                        Id = new Guid().ToString(),
                        Title = $"Executor title #{i}",
                        Body = $"Executor body #{i}"
                    };
                    _service.AddExecutor(executor);
                }
            }

            return _service;
        }
    }
}