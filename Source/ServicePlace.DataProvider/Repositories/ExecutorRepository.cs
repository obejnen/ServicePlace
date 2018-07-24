using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Models;

namespace ServicePlace.DataProvider.Repositories
{
    class ExecutorRepository
    {
        private readonly ExecutorContext _context = new ExecutorContext();

        public async Task<List<Executor>> GetExecutorList()
        {
            return await _context.Executors.ToListAsync();
        }

        public async Task<bool> AddExecutor(Executor executor)
        {
            executor.CreatedAt = DateTime.Now;
            _context.Executors.Add(executor);
            int x = await _context.SaveChangesAsync();
            return x != 0;
        }

        public async Task<bool> UpdateExecutor(Executor executor)
        {
            _context.Entry(executor).State = EntityState.Modified;
            executor.UpdatedAt = DateTime.Now;
            int x = await _context.SaveChangesAsync();
            return x != 0;
        }
    }
}
