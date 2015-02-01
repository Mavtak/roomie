using System;
using System.Net;
using System.Net.Http;
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
                //TODO: define and use static "error" object type
                var errorObject = new
                {
                    Error = new
                    {
                        Message = "Access denied.  Please sign in."
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