using System;
using System.Web;
using System.Web.Mvc;
using Roomie.Desktop.Engine;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Website.Controllers;

namespace Roomie.Web.Website.Helpers
{
    [ValidateInput(false)]
    public class RoomieBaseController : System.Web.Mvc.Controller, IRoomieController
    {
        public RoomieDatabaseContext Database { get; set; }
        protected RoomieCommandLibrary Commands;
        new public UserModel User { get; set; }

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

                ViewBag.UserAuthenticated = (User != null);
            }
            catch (Exception exception)
            {
                if (!(this is ErrorController)
                    && !(this is TestController))
                {
                    throw exception;
                }
            }

            if (HttpContext.Items[requestedControllerKey] == null)
            {
                ViewBag.NavigationItem = filterContext.RouteData.Values["controller"];
                HttpContext.Items[requestedControllerKey] = ViewBag.NavigationItem;
            }
            else
            {
                ViewBag.NavigationItem = HttpContext.Items[requestedControllerKey];
            }

            this.RefreshDatabaseHack();

            base.OnActionExecuting(filterContext);
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

        protected void addTask(ComputerModel computer, string origin, string scriptText)
        {
            var task = new TaskModel
            {
                Owner = User,
                Target = computer,
                Origin = origin,
                Script = new ScriptModel
                {
                    Mutable = false,
                    Text = scriptText
                }
            };
            Database.Tasks.Add(task);
        }

        protected override void Dispose(bool disposing)
        {
            Database.Dispose();
            base.Dispose(disposing);
        }
    }
}