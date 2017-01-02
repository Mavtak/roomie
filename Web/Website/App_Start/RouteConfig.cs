using System.Web.Mvc;
using System.Web.Routing;

namespace Roomie.Web.Website
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Source code redirect shortcut",
                url: "Source",
                defaults: new
                {
                    controller = "Home",
                    action = "Source"
                }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            // taken from http://stackoverflow.com/a/19959/1105058
            routes.MapRoute("Error", "{*url}",
                new { controller = "Error", action = "Http404" }
            );

        }
    }
}