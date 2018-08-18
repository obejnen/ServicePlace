﻿using Autofac;
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
            builder.RegisterType<ContextProvider>().As<IContextProvider>();
        }

        private void LoadRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<IdentityRepository>().As<IIdentityRepository>();
            builder.RegisterType<ProfileRepository>().As<IProfileRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<OrderResponseRepository>().As<IOrderResponseRepository>();
            builder.RegisterType<OrderCategoryRepository>().As<IOrderCategoryRepository>();
            builder.RegisterType<ProviderRepository>().As<IProviderRepository>();
            builder.RegisterType<ProviderResponseRepository>().As<IProviderResponseRepository>();
            builder.RegisterType<ProviderCategoryRepository>().As<IProviderCategoryRepository>();
            builder.RegisterType<ImageRepository>().As<IImageRepository>();
        }

        private void LoadAspNetIdentity(ContainerBuilder builder)
        {
            builder.RegisterType<UserStore<User>>().As<IUserStore<User>>();
            builder.RegisterType<RoleStore<Role>>().As<IRoleStore<Role, string>>();
            builder.RegisterType<UserStore>();
            builder.RegisterType<RoleStore>();

            builder.RegisterType<UserManager>();
            builder.RegisterType<RoleManager>();
        }
    }
}   