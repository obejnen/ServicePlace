﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ServicePlace.Website.Startup))]
namespace ServicePlace.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
