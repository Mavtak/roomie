using System;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Roomie.Web.Persistence.Database;
using Roomie.Web.WebHook;

namespace Roomie.Web.Website
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            Database.SetInitializer<EntityFrameworkRoomieDatabaseBackend>(new RoomieDatabaseInitializer());
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.ConfigureSerialization(GlobalConfiguration.Configuration);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundes(BundleTable.Bundles);
            DatabaseConfig.RegisterConnectionStrings();
            DependencyResolver.SetResolver(DependencyResolverConfig.CreateDependencyResolver());
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