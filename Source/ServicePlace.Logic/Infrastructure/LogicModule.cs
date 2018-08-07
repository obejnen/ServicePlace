using Autofac;
using ServicePlace.DataProvider.Infrastructure;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Logic.Services;

namespace ServicePlace.Logic.Infrastructure
{
    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new DataProviderModule());

            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<ProviderService>().As<IProviderService>();
        }
    }
}