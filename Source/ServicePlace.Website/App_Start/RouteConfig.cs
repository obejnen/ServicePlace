using System.Web.Mvc;
using System.Web.Routing;

namespace ServicePlace.Website
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute
            (
                name: "Show",
                url: "{controller}/{id}",
                defaults: new { action = "Show", id = UrlParameter.Optional },
                constraints: new { id = "[0-9]" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Order", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
