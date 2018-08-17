using Autofac;
using ServicePlace.DataProvider.Infrastructure;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Logic.Mappers;
using ServicePlace.Logic.Services;

namespace ServicePlace.Logic.Infrastructure
{
    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            LoadModules(builder);
            LoadMappers(builder);
            LoadServices(builder);
        }

        private void LoadModules(ContainerBuilder builder)
        {
            builder.RegisterModule(new DataProviderModule());
        }

        private void LoadMappers(ContainerBuilder builder)
        {
            builder.RegisterType<OrderMapper>().As<IOrderMapper>();
            builder.RegisterType<OrderResponseMapper>().As<IOrderResponseMapper>();
            builder.RegisterType<OrderCategoryMapper>().As<IOrderCategoryMapper>();

            builder.RegisterType<ProviderMapper>().As<IProviderMapper>();
            builder.RegisterType<ProviderResponseMapper>().As<IProviderResponseMapper>();

            builder.RegisterType<UserMapper>().As<IUserMapper>();
        }

        private void LoadServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<ImageService>().As<IImageService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<ProviderService>().As<IProviderService>();
        }
    }
}