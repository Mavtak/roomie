using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Measurements.Temperature;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Helpers;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Website.Helpers;
using System.Linq;
using Roomie.Web.Website.ViewModels;


namespace Roomie.Web.Website.Controllers
{
    
    public class DeviceController : RoomieBaseController
    {
        [WebsiteRestrictedAccess]
        public ActionResult Index(string location)
        {
            var devices = User.GetAllDevices();

            if (location != null)
            {
                devices = devices.Where(device =>
                    {
                        if (location.Equals(string.Empty))
                        {
                            return device.Location == null || device.Location.Name == null || device.Location.Name == string.Empty;
                        }

                        return device.Location != null && string.Equals(device.Location.Name, location, StringComparison.InvariantCultureIgnoreCase);
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
        public ActionResult Dim(int id, int power)
        {
            var device = this.SelectDevice(id);

            device.DimmerSwitch.SetPower(power);

            Database.SaveChanges();

            return Json(new
            {
                success = true,
                id = id
            }
            );
        }

        [HttpPost]
        [WebsiteRestrictedAccess]
        public ActionResult PowerOn(int id)
        {
            var device = this.SelectDevice(id);

            device.ToggleSwitch.PowerOn();;

            Database.SaveChanges();

            return Json(new
            {
                success = true,
                id = id
            }
            );
        }

        [HttpPost]
        [WebsiteRestrictedAccess]
        public ActionResult PowerOff(int id)
        {
            var device = this.SelectDevice(id);

            device.ToggleSwitch.PowerOff();

            Database.SaveChanges();

            return Json(new
            {
                success = true,
                id = id
            }
            );
        }

        [HttpPost]
        [WebsiteRestrictedAccess]
        public ActionResult SetThermostatMode(int id, ThermostatMode mode)
        {
            var device = this.SelectDevice(id);

            device.Thermostat.SetMode(mode);

            Database.SaveChanges();

            return Json(new
            {
                success = true,
                id = id
            }
            );
        }

        [HttpPost]
        [WebsiteRestrictedAccess]
        public ActionResult SetThermostatFanMode(int id, ThermostatFanMode mode)
        {
            var device = this.SelectDevice(id);

            device.Thermostat.Fan.SetMode(mode);

            Database.SaveChanges();

            return Json(new
            {
                success = true,
                id = id
            }
            );
        }

        [HttpPost]
        [WebsiteRestrictedAccess]
        public ActionResult SetThermostatSetpoint(int id, ThermostatSetpointType type, string temperature)
        {
            var device = this.SelectDevice(id);

            var temperatureValue = TemperatureParser.Parse(temperature);

            device.Thermostat.Setpoints.SetSetpoint(type, temperatureValue);

            Database.SaveChanges();

            return Json(new
            {
                success = true,
                id = id
            }
            );
        }

        [HttpPost]
        [WebsiteRestrictedAccess]
        public ActionResult Edit(int id, string name, string location, string type)
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
                        power = device.DimmerSwitch.Power,
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
        [WebsiteRestrictedAccess]
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
                            power = device.DimmerSwitch.Power,
                            maxPower = device.DimmerSwitch.MaxPower
                        });
                }
            }


            return Json(new
                {
                    devices = devices
                });
        }

        [HttpGet]
        public ActionResult Examples()
        {
            var devices = Persistence.Examples.Devices;

            return View("Index", devices);
        }
    }
}
