using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roomie.Web.Helpers;
using Roomie.Web.Models;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    public class NetworkController : RoomieBaseController
    {
        private static void SortDevices(NetworkModel network)
        {
            var devices = network.Devices.ToList();
            devices.Sort(new DeviceSort());
            network.Devices = devices;
        }
        private NetworkModel GetNetwork(int id)
        {
            var network = SelectNetwork(id);

            return network;
        }

        [UsersOnly]
        public ActionResult Index()
        {
            var networks = User.HomeAutomationNetworks.ToList();

            foreach (var network in networks)
            {
                SortDevices(network);
            }
            return View(networks);
        }

        [UsersOnly]
        public ActionResult Details(int id)
        {
            //TODO: Verify User
            var network = SelectNetwork(id);

            SortDevices(network);

            return View(network);
        }

        [UsersOnly]
        [HttpPost]
        public ActionResult Edit(int id, string name, string returnUrl, bool? delete)
        {
            var network = SelectNetwork(id);

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
