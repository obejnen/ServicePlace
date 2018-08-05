using System.Data.Entity;
using System.Configuration;
using Microsoft.AspNet.Identity.EntityFramework;
using ServicePlace.DataProvider.Entities;

namespace ServicePlace.DataProvider.DbContexts
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext() : base("name = DefaultConnection")
        {
        }

        public DbSet<Profile> Profiles { get; set; }
    }
}