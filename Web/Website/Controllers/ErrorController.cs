using System;
using System.Data;
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

            while (exception.InnerException != null)
                exception = exception.InnerException;

            if (exception.GetType().Equals(typeof(HttpException)))
            {
                HttpException httpException = (HttpException)exception;
                Response.StatusCode = httpException.GetHttpCode();

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        return Http404(httpException);
                }
            }

            if (exception.GetType().Equals(typeof(EntityCommandExecutionException)))
            {
                //TODO: smartly handle database errors.
            }

            return View(exception);
        }

        public ActionResult Http404(HttpException exception)
        {
            Response.StatusCode = 404;
            return View("Http404");
        }
    }
}
