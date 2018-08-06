using System;
using System.Collections.Generic;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Model;

namespace ServicePlace.Logic.Services
{
    public class ExecutorService : IExecutorService
    {
        private readonly IExecutorRepository _executorRepository;

        public ExecutorService(IExecutorRepository executorRepository)
        {
            _executorRepository = executorRepository;
        }

        public IEnumerable<Executor> Executors => _executorRepository.GetAll();

        public void Create(Executor executor)
        {
            executor.CreatedAt = DateTime.Now;
            _executorRepository.Create(executor);
        }

        public void Delete(Executor executor)
        {
            _executorRepository.Delete(executor);
        }

        public void Update(Executor executor)
        {
            _executorRepository.Update(executor);
        }

        public Executor FindById(object id)
        {
            return _executorRepository.FindById(id);
        }

        public IEnumerable<Executor> Search(string search)
        {
            return _executorRepository.Search(search);
        }

        public IEnumerable<Executor> Take(int skip, int count)
        {
            return _executorRepository.Take(skip, count);
        }
    }
}