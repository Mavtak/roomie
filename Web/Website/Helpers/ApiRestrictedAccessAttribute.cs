using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
namespace Roomie.Web.Website.Helpers
{
    public class ApiRestrictedAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var controller = actionContext.ControllerContext.Controller as IRoomieController;

            if (controller == null)
            {
                throw new Exception("could not cast controller");
            }

            if (controller.User == null)
            {
                throw new HttpException(403, "Access Denied.  Please sign in.");
            }

            base.OnActionExecuting(actionContext);
        }
    }
}