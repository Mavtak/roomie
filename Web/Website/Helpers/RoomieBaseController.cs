﻿using System;
using System.Web.Mvc;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Website.Controllers;

namespace Roomie.Web.Website.Helpers
{
    [ValidateInput(false)]
    public class RoomieBaseController : System.Web.Mvc.Controller, IRoomieController
    {
        public IRoomieDatabaseContext Database { get; set; }
        new public User User { get; set; }

        private const string userKey = "_RoomieUser";
        private const string requestedControllerKey = "_RequestedController";

        public RoomieBaseController()
        {
            Database = DependencyResolver.Current.GetService(typeof(IRoomieDatabaseContext)) as IRoomieDatabaseContext;
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
                throw exception;
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