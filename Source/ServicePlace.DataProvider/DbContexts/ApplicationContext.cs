using ServicePlace.DataProvider.Models;
using System.Data.Entity;
using System.Data.SqlClient;

namespace ServicePlace.DataProvider.DbContexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}