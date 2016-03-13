using System;
using System.Web.Mvc;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    [WebsiteRestrictedAccess]
    public class NetworkController : RoomieBaseController
    {
        public ActionResult Index()
        {
            var networks = Database.Networks.Get(User);

            foreach (var network in networks)
            {
                network.LoadDevices(Database.Devices);
            }

            return View(networks);
        }

        public ActionResult Details(int id)
        {
            //TODO: Verify User
            var network = this.SelectNetwork(id);

            return View(network);
        }

        [HttpPost]
        public ActionResult Edit(int id, string name, string returnUrl, bool? delete)
        {
            var network = this.SelectNetwork(id);

            if (delete == true)
            {
                foreach (var device in Database.Devices.Get(network))
                {
                    Database.Devices.Remove(device);
                }
                Database.Networks.Remove(network);
            }
            else
            {
                network.UpdateName(name);
                Database.Networks.Update(network);
            }

            if (String.IsNullOrEmpty(returnUrl) && HttpContext.Request.UrlReferrer != null)
            {
                returnUrl = HttpContext.Request.UrlReferrer.ToString();
            }

            if (!String.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return Json(new
            {
                id = network.Id,
                //Network = device.Network.Name,
                Address = network.Address,
                Name = network.Name
            });
        }

        [HttpPost]
        public ActionResult AddDevice(int id)
        {
            return networkAction(id, "AddDevice");
        }

        [HttpPost]
        public ActionResult RemoveDevice(int id)
        {
            return networkAction(id, "RemoveDevice");
        }

        private ActionResult networkAction(int id, string actionName)
        {
            var network = this.SelectNetwork(id);

            string text = String.Format("<HomeAutomation.{0} Network=\"{1}\" />\n<HomeAutomation.SyncWithCloud />", actionName, network.Address);

            this.AddTask(
                computer: network.AttatchedComputer,
                origin: "RoomieBot",
                scriptText: text
                );

            return AjaxSuccess();
        }
    }
}
