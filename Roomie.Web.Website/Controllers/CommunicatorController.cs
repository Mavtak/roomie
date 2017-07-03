using System.Web.Mvc;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
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
