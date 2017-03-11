﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Roomie.Common.Color;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Measurements.Temperature;
using Roomie.Web.Persistence.Helpers;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Device
{
    [ApiRestrictedAccess]
    public class DeviceController : BaseController
    {
        private IDeviceRepository _deviceRepository;
        private INetworkRepository _networkRepository;

        public DeviceController()
        {
            _deviceRepository = RepositoryFactory.GetDeviceRepository();
            _networkRepository = RepositoryFactory.GetNetworkRepository();
        }

        public IEnumerable<Persistence.Models.Device> Get(bool examples = false)
        {
            Persistence.Models.Device[] devices;

            if (examples)
            {
                devices = Persistence.Examples.Devices.ToArray();
            }
            else
            {
                var networks = _networkRepository.Get(User);
                var unsortedDevices = networks.SelectMany(_deviceRepository.Get).ToList();
                unsortedDevices.Sort(new DeviceSort());
                devices = unsortedDevices.ToArray();
            }
            
            var result = devices.Select(GetSerializableVersion);

            return result;
        }

        public Persistence.Models.Device Get(int id)
        {
            var device = SelectDevice(id);
            var result = GetSerializableVersion(device);

            return result;
        }

        public void Put(int id, [FromBody] DeviceUpdateModel update)
        {
            update = update ?? new DeviceUpdateModel();

            var device = SelectDevice(id);

            device.Update(
                location: (update.Location == null) ? device.Location : new Location(update.Location),
                name: update.Name ?? device.Name,
                type: update.Type ?? device.Type
            );

            _deviceRepository.Update(device);

            AddTask(
                computer: device.Network.AttatchedComputer,
                origin: "RoomieBot",
                scriptText: "HomeAutomation.SyncWithCloud Network=\"" + device.Network.Address + "\""
            );
        }

        public void Post(int id, string action, [FromBody] DeviceActionOptions options,
            [FromUri] string color = null,
            [FromUri] string mode = null,
            [FromUri] int? power = null,
            [FromUri] string temperature = null,
            [FromUri] string type = null
            )
        {
            options = options ?? new DeviceActionOptions();
            options.Color = options.Color ?? color;
            options.Mode = options.Mode ?? mode;
            options.Power = options.Power ?? power;
            options.Temperature = options.Temperature ?? temperature;
            options.Type = options.Type ?? type;

            IDevice device = this.SelectDevice(id);

            switch (action)
            {
                case "Dim":
                    device.MultilevelSwitch.SetPower(options.Power.Value);
                    break;

                case "PollBinarySensor":
                    device.BinarySensor.Poll();
                    break;

                case "PollDevice":
                    device.Poll();
                    break;

                case "PollHumiditySensor":
                    device.HumiditySensor.Poll();
                    break;

                case "PollIlluminanceSensor":
                    device.IlluminanceSensor.Poll();
                    break;

                case "PollPowerSensor":
                    device.PowerSensor.Poll();
                    break;

                case "PollTemperatureSensor":
                    device.TemperatureSensor.Poll();
                    break;

                case "PowerOn":
                    device.BinarySwitch.SetPower(BinarySwitchPower.On);
                    break;

                case "PowerOff":
                    device.BinarySwitch.SetPower(BinarySwitchPower.Off);
                    break;

                case "SetColor":
                    device.ColorSwitch.SetValue(options.Color.ToColor());
                    break;

                case "SetThermostatFanMode":
                    device.Thermostat.Fan.SetMode(options.Mode.ToThermostatFanMode());
                    break;

                case "SetThermostatMode":
                    device.Thermostat.Core.SetMode(options.Mode.ToThermostatMode());
                    break;

                case "SetThermostatSetpoint":
                    device.Thermostat.Setpoints.SetSetpoint(options.Type.ToSetpointType(), options.Temperature.ToTemperature());
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        private static Persistence.Models.Device GetSerializableVersion(Persistence.Models.Device device)
        {
            return Persistence.Models.Device.Create(device.Id, device);
        }

        private Persistence.Models.Device SelectDevice(int id)
        {
            var device = _deviceRepository.Get(User, id);

            if (device == null)
            {
                throw new HttpException(404, "Device not found");
            }

            return device;
        }
    }
}