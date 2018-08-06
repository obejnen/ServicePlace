using System;
using System.Collections.Generic;
using ServicePlace.Common.Enums;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Model;

namespace ServicePlace.Logic.Services
{
    public class ExecutorService : IExecutorService
    {
        private readonly IExecutorRepository<Executor, int, ResponseType> _executorRepository;

        public ExecutorService(IExecutorRepository<Executor, int, ResponseType> executorRepository)
        {
            _executorRepository = executorRepository;
        }

        public IEnumerable<Executor> Executors => _executorRepository.GetAll();

        public ResponseType Create(Executor executor)
        {
            executor.CreatedAt = DateTime.Now;
            return _executorRepository.Create(executor);
        }

        public ResponseType Delete(Executor executor)
        {
            return _executorRepository.Delete(executor);
        }

        public ResponseType Update(Executor executor)
        {
            return _executorRepository.Update(executor);
        }

        public Executor FindById(int id)
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