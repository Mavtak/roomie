using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;

using Roomie.Web.Models;
using Roomie.Web.Helpers;
using Roomie.Web.WebHook;

namespace Roomie.Web.Website
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

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
                        controller = "(Network|Device|Computer|Script|Task|ControlPanel)"
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

        }

        protected void Application_Start()
        {
            Database.SetInitializer<RoomieDatabaseContext>(new RoomieDatabaseInitializer());
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //var database = new RoomieDatabaseContext();
            //database.CreateTables();
            //database.Seed();
            //database.SaveChanges();

            WebhookUtilities.StartServer();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            
            
            try
            {
                IController errorController = new Roomie.Web.Website.Controllers.ErrorController();
                Response.Clear();
                RouteData routeData = new RouteData();
                routeData.Values.Add("controller", "Error");
                routeData.Values.Add("action", "UnhandledException");
                routeData.Values.Add("exception", exception);
                Response.TrySkipIisCustomErrors = true;
                errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            }
            catch
            {
                try
                {
                    IController errorController = new Roomie.Web.Website.Controllers.ErrorController();
                    Response.Clear();
                    RouteData routeData = new RouteData();
                    routeData.Values.Add("controller", "Error");
                    routeData.Values.Add("action", "UnknownError");
                    Response.TrySkipIisCustomErrors = true;
                    errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                }
                catch
                {
                    Response.Write("Totally unhandled error. " + exception);
                }
            }
            Server.ClearError();

        }
    }
}