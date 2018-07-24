using System.Data.Entity;
using ServicePlace.DataProvider.Models;

namespace ServicePlace.DataProvider.DbContexts
{
    class ExecutorContext : DbContext
    {
        public ExecutorContext() : base("DefaultConnection")
        {

        }

        public DbSet<Executor> Executors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
