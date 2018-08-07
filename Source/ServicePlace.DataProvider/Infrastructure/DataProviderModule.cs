using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServicePlace.Model.DataModels;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.DataProvider.Managers;
using ServicePlace.DataProvider.Mappers;
using ServicePlace.DataProvider.Repositories;
using ServicePlace.DataProvider.Stores;

namespace ServicePlace.DataProvider.Infrastructure
{
    public class DataProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationContext>().AsSelf();
            builder.RegisterType<OrderResponseMapper>().AsSelf();

            builder.RegisterType<UserStore<User>>().As<IUserStore<User>>();
            builder.RegisterType<RoleStore<Role>>().As<IRoleStore<Role, string>>();
            builder.RegisterType<ProfileManager>().As<IProfileManager>();

            builder.RegisterType<UserStore>();
            builder.RegisterType<UserManager>();
            builder.RegisterType<RoleManager>();


            builder.RegisterType<IdentityRepository>().As<IIdentityRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<OrderResponseRepository>().As<IOrderResponseRepository>();
            builder.RegisterType<ProviderRepository>().As<IProviderRepository>();
        }
    }
}