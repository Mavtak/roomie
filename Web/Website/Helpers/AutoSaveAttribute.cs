using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Roomie.Web.Website.Helpers
{
    public class AutoSaveAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var controller = actionExecutedContext.ActionContext.ControllerContext.Controller as IRoomieController;

            if (controller == null)
            {
                throw new Exception("Could not save changes.");
            }

            controller.Database.SaveChanges();

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}