﻿using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Roomie.Common.Api.Models;
using Roomie.Web.Backend.Controllers.Api;

namespace Roomie.Web.Backend.Helpers
{
    public class ApiRestrictedAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var controller = actionContext.ControllerContext.Controller as BaseController;

            if (controller == null)
            {
                throw new Exception("could not cast controller");
            }

            if (controller.User == null)
            {
                var errorObject = Response.CreateError("Access denied.  Please sign in.",
                    "must-sign-in"
                );

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