using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Website.Helpers;
using Roomie.Web.Website.ViewModels;

namespace Roomie.Web.Website.Controllers
{
    
    public class DeviceController : RoomieBaseController
    {
        [WebsiteRestrictedAccess]
        public ActionResult Index(string search)
        {
            var devices = Database.GetDevicesForUser(User).AsEnumerable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();

                devices = devices.Where(device =>
                    {
                        var comparisonString = device.BuildVirtualAddress(false, false).ToLower();
                        var result = comparisonString.Contains(search);

                        return result;
                    });
            }

            return View(devices);
        }

        [WebsiteRestrictedAccess]
        public ActionResult Details(int id)
        {
            var device = this.SelectDevice(id);

            return View(device);
        }

        [HttpPost]
        [WebsiteRestrictedAccess]
        public ActionResult Edit(int id, string name, string location, string type)
        {
            var device = this.SelectDevice(id);

            device.Update(
                location: (location == null) ? device.Location : new Location(location),
                name: name ?? device.Name,
                type: type ?? device.Type
            );

            this.AddTask(
                computer: device.Network.AttatchedComputer,
                origin: "RoomieBot",
                scriptText: "HomeAutomation.SyncWithCloud Network=\"" + device.Network.Address + "\""
            );

            Database.Devices.Update(device);

            Database.SaveChanges();

            return Json(new
            {
                id = device.Id,
                //Network = device.Network.Name,
                Address = device.Address,
                Name = device.Name
            });
        }

        [HttpGet]
        [WebsiteRestrictedAccess]
        public ActionResult IndexAjax()
        {
            var replacements = new List<object>();
            var devices = new List<object>();
            //TODO: improve this (smarter state?)

            foreach (var network in Database.Networks.Get(User))
            {
                foreach (var device in Database.Devices.Get(network))
                {
                    var viewModel = new DeviceViewModel(device, Url);
                    var html = RenderPartialViewToString("Partials/Device/Device", viewModel);
                    replacements.Add(new
                    {
                        id = device.DivId,
                        html = html
                    });

                    devices.Add(new
                    {
                        id = device.DivId,
                        power = device.MultilevelSwitch.Power,
                        isAvailable = device.IsAvailable
                    });
                    
                }
            }

            return Json(new
                {
                    replacements = replacements,
                    devices = devices
                },
                JsonRequestBehavior.AllowGet
            );
        }

        [HttpGet]
        public ActionResult Examples()
        {
            var devices = Persistence.Examples.Devices;

            return View("Index", devices);
        }
    }
}
