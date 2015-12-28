using System.Web.Mvc;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    public class HelpController : RoomieBaseController
    {
        public ActionResult RoomieScript()
        {
            return View();
        }

        public ActionResult Commands()
        {
            base.Commands = UpdateCommands();

            var commands = base.Commands;

            return View(commands);
        }

        public ActionResult DeviceAddresses()
        {
            return View();
        }

    }
}
