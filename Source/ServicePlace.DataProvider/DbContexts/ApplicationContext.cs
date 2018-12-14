using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServicePlace.DataProvider.Managers;
using ServicePlace.DataProvider.Migrations;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.DbContexts
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationContext, Configuration>());
        }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderCategory> OrderCategories { get; set; }

        public DbSet<OrderResponse> OrderResponses { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<ProviderResponse> ProviderResponses { get; set; }

        public DbSet<ProviderCategory> ProviderCategories { get; set; }

        public DbSet<Image> Photos { get; set; }
    }
}