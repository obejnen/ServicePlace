using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity;
using ServicePlace.Logic.Services;
using ServicePlace.Logic.Interfaces;

[assembly: OwinStartup(typeof(ServicePlace.Website.App_Start.Startup))]

namespace ServicePlace.Website.App_Start
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }

        public IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService();
        }
    }
}