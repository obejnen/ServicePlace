using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Models;
using ServicePlace.Logic.Stores;

namespace ServicePlace.Website
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, UserRole>()
                .AddDefaultTokenProviders();
            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<UserRole>, RoleStore>();
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationContext context, SignInManager<User> s)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                if (s.UserManager.FindByNameAsync("dev").Result == null)
                {
                    var result = s.UserManager.CreateAsync(new User
                    {
                        UserName = "dev",
                        Email = "dev@app.com"
                    }, "Aut94L#G-a").Result;
                }
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
