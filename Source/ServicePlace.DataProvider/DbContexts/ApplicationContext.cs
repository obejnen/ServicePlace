using Microsoft.EntityFrameworkCore;
using ServicePlace.DataProvider.Models;

namespace ServicePlace.DataProvider.DbContexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}