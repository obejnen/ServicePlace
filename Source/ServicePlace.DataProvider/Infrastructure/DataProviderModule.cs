using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServicePlace.DataProvider.ContextProviders;
using ServicePlace.Model.DataModels;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.DataProvider.Managers;
using ServicePlace.DataProvider.Repositories;
using ServicePlace.DataProvider.Stores;

namespace ServicePlace.DataProvider.Infrastructure
{
    public class DataProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            LoadContexts(builder);
            LoadAspNetIdentity(builder);
            LoadRepositories(builder);
        }

        private void LoadContexts(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationContext>().AsSelf().SingleInstance();
            builder.RegisterType<CommitProvider>().As<IContextProvider>();
        }

        private void LoadRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<IdentityRepository>().As<IIdentityRepository>().InstancePerDependency();
            builder.RegisterType<ProfileRepository>().As<IProfileRepository>().InstancePerDependency();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerDependency();
            builder.RegisterType<OrderResponseRepository>().As<IOrderResponseRepository>().InstancePerDependency();
            builder.RegisterType<OrderCategoryRepository>().As<IOrderCategoryRepository>().InstancePerDependency();
            builder.RegisterType<ProviderRepository>().As<IProviderRepository>().InstancePerDependency();
            builder.RegisterType<ProviderResponseRepository>().As<IProviderResponseRepository>().InstancePerDependency();
            builder.RegisterType<ProviderCategoryRepository>().As<IProviderCategoryRepository>().InstancePerDependency();
            builder.RegisterType<ImageRepository>().As<IImageRepository>().InstancePerDependency();
        }

        private void LoadAspNetIdentity(ContainerBuilder builder)
        {
            builder.RegisterType<UserStore<User>>().As<IUserStore<User>>().InstancePerDependency();
            builder.RegisterType<RoleStore<Role>>().As<IRoleStore<Role, string>>().InstancePerDependency();
            builder.RegisterType<UserStore>().InstancePerDependency();
            builder.RegisterType<RoleStore>().InstancePerDependency();

            builder.RegisterType<UserManager>().InstancePerDependency();
            builder.RegisterType<RoleManager>().InstancePerDependency();
        }
    }
}   