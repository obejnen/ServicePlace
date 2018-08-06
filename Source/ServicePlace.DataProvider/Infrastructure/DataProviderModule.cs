using Autofac;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CommonModels = ServicePlace.Model;
using DataModels = ServicePlace.DataProvider.Entities;
using ServicePlace.Common.Enums;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.DataProvider.Managers;
using ServicePlace.DataProvider.Repositories;

namespace ServicePlace.DataProvider.Infrastructure
{
    public class DataProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationContext>().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<UserStore<DataModels.User>>().As<IUserStore<DataModels.User>>();
            builder.RegisterType<RoleStore<DataModels.Role>>().As<IRoleStore<DataModels.Role, string>>();
            builder.RegisterType<UserManager>();
            builder.RegisterType<RoleManager>();
            builder.RegisterType<ProfileManager>().As<IProfileManager>();
            builder.RegisterType<IdentityRepository>().As<IIdentityRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository<CommonModels.Order, int, ResponseType>>()
                .WithParameter("context", new ApplicationContext());
        }
    }
}