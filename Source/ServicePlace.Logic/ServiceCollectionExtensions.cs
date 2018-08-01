using Microsoft.Extensions.DependencyInjection;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.Model;

namespace ServicePlace.Logic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>();
            services.AddTransient<DataProvider.Interfaces.IRolesRepository, DataProvider.Repositories.RolesRepository>();
            services.AddTransient<DataProvider.Interfaces.IUsersRepository, DataProvider.Repositories.UsersRepository>();
            services.AddTransient<DataProvider.Interfaces.IUsersRolesRepository, DataProvider.Repositories.UsersRolesRepository>();
            return services;
        }
    }
}
