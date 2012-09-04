using System.Web.Mvc;
using Roomie.Web.Helpers;

namespace Roomie.Web.Api.Controllers
{
    public class CommunicatorController : Controller
    {
        //
        // GET: /Communicator/

        public ActionResult Index()
        {
            return new WebHookViewResult();
        }

    }
}
