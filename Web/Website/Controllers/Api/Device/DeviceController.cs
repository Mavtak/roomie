using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Roomie.Common.Color;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Measurements.Temperature;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api
{
    [ApiRestrictedAccess]
    public class DeviceController : RoomieBaseApiController
    {
        public IEnumerable<Device> Get(bool examples = false)
        {
            var devices = examples ? Persistence.Examples.Devices : Database.GetDevicesForUser(User);
            var result = devices.Select(GetSerializableVersion);

            return result;
        }

        public Device Get(int id)
        {
            var device = this.SelectDevice(id);
            var result = GetSerializableVersion(device);

            return result;
        }

        public void Put(int id, [FromBody] DeviceUpdateModel update)
        {
            update = update ?? new DeviceUpdateModel();

            var device = this.SelectDevice(id);

            device.Update(
                location: (update.Location == null) ? device.Location : new Location(update.Location),
                name: update.Name ?? device.Name,
                type: update.Type ?? device.Type
            );

            Database.Devices.Update(device);

            this.AddTask(
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

        private static Device GetSerializableVersion(Device device)
        {
            return Device.Create(device.Id, device);
        }
    }
}
