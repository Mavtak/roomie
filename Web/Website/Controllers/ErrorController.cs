using System;
using System.Data.Entity.Core;
using System.Web;
using System.Web.Mvc;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    public class ErrorController : RoomieBaseController
    {
        public ActionResult Index(Exception exception)
        {
            if (exception != null)
                return UnhandledException(exception);
            return UnknownError();
        }

        public ActionResult UnknownError()
        {
            return View();
        }

        public ActionResult UnhandledException(Exception exception)
        {
            if (exception == null)
            {
                return UnknownError();
            }

            if (exception.GetType().Equals(typeof(HttpException)))
            {
                HttpException httpException = (HttpException)exception;
                Response.StatusCode = httpException.GetHttpCode();
            }

            if (exception.GetType().Equals(typeof(EntityCommandExecutionException)))
            {
                //TODO: smartly handle database errors.
            }

            return Content(exception.ToString());
        }
    }
}
