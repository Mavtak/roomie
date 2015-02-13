using System.Web.Mvc;

namespace Roomie.Web.Website.Controllers
{
    public class AngularController : Controller
    {
        public ActionResult Index()
        {
            return View("App");
        }
    }
}