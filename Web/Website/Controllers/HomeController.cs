using System.Web.Mvc;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    public class HomeController : RoomieBaseController
    {
        public ActionResult Index()
        {
            if(User == null)
            {
                return Redirect("/angular#/help/about");
            }

            return Redirect("/angular#/devices");
        }
    }
}
