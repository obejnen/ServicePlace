using Microsoft.EntityFrameworkCore;
using ServicePlace.DataProvider.Models;

namespace ServicePlace.DataProvider.DbContexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : this(new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ServicePlaceDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .Options)
        {
        }

        private ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}