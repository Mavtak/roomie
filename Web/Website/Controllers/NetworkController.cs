using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roomie.Web.Models;
using Roomie.Web.Models.Helpers;
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
            var networks = User.HomeAutomationNetworks.ToList();

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

    }
}
