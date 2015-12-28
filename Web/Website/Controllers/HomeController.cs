using System.Web.Mvc;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    public class HomeController : RoomieBaseController
    {
        public ActionResult Index()
        {
            return Redirect("/angular#/help/about");
        }

        public ActionResult Source()
        {
            return Redirect("http://github.com/Mavtak/Roomie");
        }
    }
}
