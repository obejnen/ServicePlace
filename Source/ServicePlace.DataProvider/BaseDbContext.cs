using System;
using System.Data.Entity;
using ServicePlace.DataProvider.Models;

namespace ServicePlace.DataProvider
{
    class BaseDbContext : DbContext
    {
        public BaseDbContext() : base(String.Empty)
        {

        }

        public virtual DbSet<Executor> Executors { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }
}
