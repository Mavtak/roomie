﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Measurements.Temperature;
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
        public ActionResult Dim(int id, int power)
        {
            return DeviceAction(id, device => device.DimmerSwitch.SetPower(power));
        }

        [HttpPost]
        [WebsiteRestrictedAccess]
        public ActionResult PowerOn(int id)
        {
            return DeviceAction(id, device => device.ToggleSwitch.SetPower(BinarySwitchPower.On));
        }

        [HttpPost]
        [WebsiteRestrictedAccess]
        public ActionResult PowerOff(int id)
        {
            return DeviceAction(id, device => device.ToggleSwitch.SetPower(BinarySwitchPower.Off));
        }

        [HttpPost]
        [WebsiteRestrictedAccess]
        public ActionResult SetThermostatMode(int id, ThermostatMode mode)
        {
            return DeviceAction(id, device => device.Thermostat.SetMode(mode));
        }

        [HttpPost]
        [WebsiteRestrictedAccess]
        public ActionResult SetThermostatFanMode(int id, ThermostatFanMode mode)
        {
            return DeviceAction(id, device => device.Thermostat.Fan.SetMode(mode));
        }

        [HttpPost]
        [WebsiteRestrictedAccess]
        public ActionResult SetThermostatSetpoint(int id, ThermostatSetpointType type, string temperature)
        {
            var temperatureValue = TemperatureParser.Parse(temperature);

            return DeviceAction(id, device => device.Thermostat.Setpoints.SetSetpoint(type, temperatureValue));
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

        private ActionResult DeviceAction(int id, Action<DeviceModel> action)
        {
            var device = this.SelectDevice(id);

            action(device);

            Database.SaveChanges();

            return Json(new
            {
                success = true,
                id = id
            }
            );
        }
    }
}
