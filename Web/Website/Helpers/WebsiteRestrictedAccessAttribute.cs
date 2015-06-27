using System;
using System.Web;
using System.Web.Mvc;

namespace Roomie.Web.Website.Helpers
{
    public class WebsiteRestrictedAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as IRoomieController;

            if (controller == null)
            {
                throw new Exception("could not cast controller");
            }

            if (controller.User == null)
            {
                throw new HttpException(403, "Access Denied.  Please sign in.");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}