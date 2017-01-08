using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Roomie.Web.Website.Controllers.Api;

namespace Roomie.Web.Website.Helpers
{
    public class ApiRestrictedAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var controller = actionContext.ControllerContext.Controller as RoomieBaseApiController;

            if (controller == null)
            {
                throw new Exception("could not cast controller");
            }

            if (controller.User == null)
            {
                var errorObject = new[]
                {
                    new Error
                    {
                        FriendlyMessage = "Access denied.  Please sign in.",
                        Type = "must-sign-in",
                    }
                };

                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    //TODO: get MediaTypeFormatter from actionContext
                    Content = new ObjectContent(typeof (object), errorObject, actionContext.ControllerContext.Configuration.Formatters.JsonFormatter)
                };
            }

            base.OnActionExecuting(actionContext);
        }
    }
}