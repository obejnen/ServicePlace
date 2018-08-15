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
            builder.RegisterType<ProviderResponseMapper>().AsSelf();

            builder.RegisterType<UserStore<User>>().As<IUserStore<User>>();
            builder.RegisterType<RoleStore<Role>>().As<IRoleStore<Role, string>>();
            builder.RegisterType<ProfileManager>().As<IProfileManager>();

            builder.RegisterType<UserStore>();
            builder.RegisterType<RoleStore>();
            builder.RegisterType<UserManager>();
            builder.RegisterType<RoleManager>();


            builder.RegisterType<IdentityRepository>().As<IIdentityRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<ProviderRepository>().As<IProviderRepository>();
            builder.RegisterType<OrderResponseRepository>().As<IOrderResponseRepository>();
            builder.RegisterType<ProviderResponseRepository>().As<IProviderResponseRepository>();
        }
    }
}   