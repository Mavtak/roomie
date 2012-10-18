using System.Web.Mvc;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    public class HomeController : RoomieBaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Source()
        {
            return Redirect("http://github.com/Mavtak/Roomie");
        }
    }
}
