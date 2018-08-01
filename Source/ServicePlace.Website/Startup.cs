using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServicePlace.Model;
using ServicePlace.Logic.Stores;
using ServicePlace.Logic;
using Autofac;
using AutoMapper;

namespace ServicePlace.Website
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            services.AddIdentity<User, Role>()
                .AddUserManager<Infrastructure.UserManager>()
                .AddSignInManager<Infrastructure.SignInManager>()
                .AddRoleManager<Infrastructure.RoleManager>()
                .AddDefaultTokenProviders();
            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<Role>, RoleStore>();

            services.AddRepositories();

            services.AddTransient<Logic.Interfaces.IRoleStore, RoleStore>();
            services.AddTransient<Logic.Interfaces.IUserStore, UserStore>();
            services.AddMvc();
            services.AddAutoMapper();

            services.AddTransient<IOrderService, OrderService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
            });
        }
    }
}
