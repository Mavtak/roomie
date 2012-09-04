using System.Web.Mvc;
using Roomie.Web.Helpers;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    public class ControlPanelController : RoomieBaseController
    {
        //
        // GET: /ControlPanel/

        public ActionResult Index()
        {
            return View(model: null);
        }

    }
}
