using System;
using System.Data.Entity;

namespace ServicePlace.DataProvider
{
    class BaseDbContext : DbContext
    {
        public BaseDbContext() : base(String.Empty)
        {

        }

        public virtual DbSet<>
    }
}
