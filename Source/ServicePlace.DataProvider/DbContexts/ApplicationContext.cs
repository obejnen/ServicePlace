﻿using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServicePlace.DataProvider.Managers;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.DbContexts
{
    public class ApplicationContext : IdentityDbContext<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public ApplicationContext() : base("name = DefaultConnection")
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<OrderResponse> OrderResponses { get; set; }
        public DbSet<ProviderResponse> ProviderResponses { get; set; }
    }
}