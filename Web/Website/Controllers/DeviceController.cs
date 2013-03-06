using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Helpers;
using Roomie.Web.Persistence.Models;
using Roomie.Web.ViewModels;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    [WebsiteRestrictedAccess]
    public class DeviceController : RoomieBaseController
    {
        public ActionResult Index()
        {
            var devices = User.GetAllDevices();

            return View(devices);
        }

        public ActionResult Details(int id)
        {
            var device = this.SelectDevice(id);

            return View(device);
        }

        [HttpPost]
        public ActionResult Dim(int id, int power, string returnUrl)
        {
            var device = this.SelectDevice(id);

            addTask(
                computer: device.Network.AttatchedComputer,
                origin: "Web Interface",
                scriptText: String.Format(//TODO: improve this script
                    "HomeAutomation.Dim Device=\"{0}\" Power=\"{1}\"",
                    device.BuildVirtualAddress(true, true), power)
            );

            Database.SaveChanges();

            return Json(new
            {
                success = true,
                id = id
            }
            );
        }

        [HttpPost]
        public ActionResult PowerOn(int id, string returnUrl)
        {
            var device = this.SelectDevice(id);

            addTask(
                computer: device.Network.AttatchedComputer,
                origin: "Web Interface",
                scriptText: String.Format(//TODO: improve this script
                    "HomeAutomation.PowerOn Device=\"{0}\"",
                    device.BuildVirtualAddress(true, true))
            );

            Database.SaveChanges();

            return Json(new
            {
                success = true,
                id = id
            }
            );
        }

        [HttpPost]
        public ActionResult PowerOff(int id, string returnUrl)
        {
            var device = this.SelectDevice(id);

            addTask(
                computer: device.Network.AttatchedComputer,
                origin: "Web Interface",
                scriptText: String.Format(//TODO: improve this script
                    "HomeAutomation.PowerOff Device=\"{0}\"",
                    device.BuildVirtualAddress(true, true))
            );

            Database.SaveChanges();

            return Json(new
            {
                success = true,
                id = id
            }
            );
        }

        [HttpPost]
        public ActionResult Edit(int id, string name, string location, string type, string returnUrl)
        {
            var device = this.SelectDevice(id);

            if (name != null)
            {
                device.Name = name;
            }

            if (location != null)
            {
                device.Location = Database.GetDeviceLocation(User, location);
            }

            if (type != null)
            {
                device.Type = type;
            }

            addTask(
                computer: device.Network.AttatchedComputer,
                origin: "RoomieBot",
                scriptText: "HomeAutomation.SyncWithCloud"
            );

            Database.SaveChanges();

            if (String.IsNullOrEmpty(returnUrl) && HttpContext.Request.UrlReferrer != null)
            {
                returnUrl = HttpContext.Request.UrlReferrer.ToString();
            }

            if (!String.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return Json(new
            {
                id = device.Id,
                //Network = device.Network.Name,
                Address = device.Address,
                Name = device.Name
            });
        }

        [HttpGet]
        public ActionResult IndexAjax()
        {
            var replacements = new List<object>();
            var devices = new List<object>();
            //TODO: improve this (smarter state?)
            foreach (var network in User.HomeAutomationNetworks)
            {
                foreach (DeviceModel device in network.Devices)
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
                        power = device.Power,
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

        [HttpPost]
        public ActionResult IndexAjaxJson()
        {
            var devices = new List<object>();
            foreach (var network in User.HomeAutomationNetworks)
            {
                foreach (var device in network.Devices)
                {
                    devices.Add(new
                        {
                            id = device.DivId,
                            name = device.Name,
                            location = device.Location,
                            isAvailable = device.IsAvailable,
                            power = device.Power,
                            maxPower = device.MaxPower
                        });
                }
            }


            return Json(new
                {
                    devices = devices
                });
        }
    }
}
