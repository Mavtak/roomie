using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roomie.Web.Models;
using Roomie.Desktop.Engine;
using Roomie.Web.Helpers;

namespace Roomie.Web.Website.Helpers
{
    public class RoomieBaseController : System.Web.Mvc.Controller
    {
        protected RoomieDatabaseContext Database;
        protected RoomieCommandLibrary Commands;
        new protected UserModel User;

        private const string userKey = "_RoomieUser";
        private const string requestedControllerKey = "_RequestedController";

        public RoomieBaseController()
        {
            Database = new RoomieDatabaseContext();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (HttpContext.Items[userKey] == null)
                {
                    User = UserUtilities.GetCurrentUser(Database);
                    ViewBag.User = User;
                    HttpContext.Items[userKey] = User;
                }
                else
                {
                    ViewBag.User = HttpContext.Items[userKey];
                }
            }
            catch (Exception exception)
            {
                if (!this.GetType().Name.Equals("ErrorController")
                    && !this.GetType().Name.Equals("TestController"))
                    throw exception;
            }

            ViewBag.UserAuthenticated = (User != null);


            if (HttpContext.Items[requestedControllerKey] == null)
            {
                ViewBag.NavigationItem = filterContext.RouteData.Values["controller"];
                HttpContext.Items[requestedControllerKey] = ViewBag.NavigationItem;
            }
            else
            {
                ViewBag.NavigationItem = HttpContext.Items[requestedControllerKey];
            }

            try
            {
                //TODO: remove this hack;
                var networks = User.HomeAutomationNetworks.ToList();
                var devices = Database.Devices.ToList();
                var locations = Database.DeviceLocations.ToList();
                var computers = User.Computers.ToList();
                var users = Database.Users.ToList();
                var scripts = Database.Scripts.ToList();
            }
            catch
            { }

            // Redirect to sign in page if the action is users-only.
            if (User == null)
            {
                var attributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(UsersOnlyAttribute), false);
                if (attributes.Length > 0)
                {
                    filterContext.Result = RedirectToAction(
                        actionName: "SignIn",
                        controllerName: "User"
                    );
                }
            }

            base.OnActionExecuting(filterContext);
        }

        protected DeviceModel SelectDevice(int id)
        {
            var device = Database.Devices.Find(id);
            if (device == null)
            {
                throw new HttpException(404, "Device not found");
            }

            var users = Database.Users.ToList();

            if (device.Network == null)
            {
                throw new HttpException(404, "Device not found");
            }

            if (device.Network.Owner != User)
            {
                throw new HttpException(404, "Device not found");
            }

            return device;
        }

        protected NetworkModel SelectNetwork(int id)
        {
            var network = Database.Networks.Find(id);
            if (network == null)
            {
                throw new HttpException(404, "Device not found");
            }

            if (network.Owner != User)
            {
                throw new Exception("You do not own that network.");
            }

            return network;
        }

        protected SavedScriptModel SelectSavedScript(int id)
        {
            var script = Database.SavedScripts.Find(id);
            if (script == null)
            {
                throw new HttpException(404, "Script not found");
            }

            if (script.Owner != User)
            {
                throw new HttpException(404, "Script not found");
            }

            return script;
        }

        protected ComputerModel SelectComputer(int id)
        {
            var computer = Database.Computers.Find(id);
            if (computer == null)
            {
                throw new HttpException(404, "Computer not found");
            }

            if (computer.Owner != User)
            {
                throw new HttpException(404, "Computer not found");
            }

            return computer;
        }

        protected JsonResult AjaxSuccess()
        {
            return Json(new
                {
                    success = true
                }
            );
        }

        protected RoomieCommandLibrary UpdateCommands()
        {
            var binDirectory = System.Web.Hosting.HostingEnvironment.MapPath("~/bin");
            var commands = new RoomieCommandLibrary();
            commands.AddCommandsFromPluginFolder(binDirectory);
            return commands;
        }

        /// <summary>
        /// from http://craftycodeblog.com/2010/05/15/asp-net-mvc-render-partial-view-to-string/
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (var writer = new System.IO.StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, writer);
                viewResult.View.Render(viewContext, writer);

                return writer.GetStringBuilder().ToString();
            }
        }

        protected override void Dispose(bool disposing)
        {
            Database.Dispose();
            base.Dispose(disposing);
        }
    }
}