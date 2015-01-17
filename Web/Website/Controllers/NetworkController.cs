using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roomie.Web.Persistence.Helpers;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    [WebsiteRestrictedAccess]
    public class NetworkController : RoomieBaseController
    {
        private NetworkModel GetNetwork(int id)
        {
            var network = this.SelectNetwork(id);

            return network;
        }

        public ActionResult Index()
        {
            var networks = Database.Networks.Get(User);

            foreach (var network in networks)
            {
                network.SortDevices();
            }

            return View(networks);
        }

        public ActionResult Details(int id)
        {
            //TODO: Verify User
            var network = this.SelectNetwork(id);

            network.SortDevices();

            return View(network);
        }

        [HttpPost]
        public ActionResult Edit(int id, string name, string returnUrl, bool? delete)
        {
            var network = this.SelectNetwork(id);

            network.Name = name;
            if (delete == true)
            {
                foreach (DeviceModel device in network.Devices.ToList())
                {
                    Database.Devices.Remove(device);
                }
                Database.Networks.Remove(network);
            }
            Database.SaveChanges();

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

            Database.SaveChanges();

            return AjaxSuccess();
        }
    }
}
