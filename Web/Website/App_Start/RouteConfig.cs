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
                name: "Sign In Shortcut",
                url: "SignIn",
                defaults: new
                {
                    controller = "User",
                    action = "SignIn"
                }
            );

            routes.MapRoute(
                name: "Sign Out Shortcut",
                url: "SignOut",
                defaults: new
                {
                    controller = "User",
                    action = "SignOut"
                }
            );

            routes.MapRoute(
                name: "Authenticate Shortcut",
                url: "Authenticate",
                defaults: new
                {
                    controller = "User",
                    action = "Authenticate"
                }
            );

            routes.MapRoute(
                name: "Account Settings Shortcut",
                url: "Account",
                defaults: new
                {
                    controller = "User",
                    action = "Edit"
                }
            );

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
                name: "Index shortcut",
                url: "{controller}s",
                defaults: new
                {
                    action = "Index"
                },
                constraints: new
                {
                    controller = "(Network|Device|Computer)"
                }
            );

            routes.MapRoute(
                name: "Details Shortcut",
                url: "{controller}/{id}/{name}",
                defaults: new
                {
                    action = "Details",
                    name = UrlParameter.Optional
                },
                constraints: new
                {
                    id = "[0-9]*"
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