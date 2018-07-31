using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePlace.Logic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<DataProvider.Interfaces.IRolesRepository, DataProvider.Repositories.RolesRepository>();
            services.AddTransient<DataProvider.Interfaces.IUsersRepository, DataProvider.Repositories.UsersRepository>();
            services.AddTransient<DataProvider.Interfaces.IUsersRolesRepository, DataProvider.Repositories.UsersRolesRepository>();
            return services;
        }
    }
}
