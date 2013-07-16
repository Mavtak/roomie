using System;
using System.Collections.Generic;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Temperature;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence
{
    public static class Examples
    {
        public static IEnumerable<DeviceModel> Devices
        {
            get
            {
                var thermostat = new DeviceModel
                    {
                        Name = "A Thermostat with all data",
                        Type = DeviceType.Thermostat,
                    };

                
                thermostat.Thermostat.Temperature = new FahrenheitTemperature(75);
                thermostat.Thermostat.Mode = ThermostatMode.Auto;
                thermostat.Thermostat.SupportedModes = new[] { ThermostatMode.Heat, ThermostatMode.Cool, ThermostatMode.Auto, ThermostatMode.FanOnly, ThermostatMode.Off};
                thermostat.Thermostat.CurrentAction = ThermostatCurrentAction.Cooling;
                thermostat.Thermostat.Fan.Mode = ThermostatFanMode.Auto;
                thermostat.Thermostat.Fan.SupportedModes = new[] {ThermostatFanMode.Auto, ThermostatFanMode.On,};
                thermostat.Thermostat.Fan.CurrentAction = ThermostatFanCurrentAction.On;
                thermostat.Thermostat.Setpoints.Add(SetpointType.Cool, new FahrenheitTemperature(74));
                thermostat.Thermostat.Setpoints.Add(SetpointType.Heat, new FahrenheitTemperature(70));

                var devices = new[]
                    {
                        new DeviceModel
                            {
                                Name = "A Toggle Switch that is on",
                                Type = DeviceType.Switch,
                                Power = 50
                            },
                        new DeviceModel
                            {
                                Name = "A Toggle Switch that is off",
                                Type = DeviceType.Switch,
                                Power = 0
                            },
                        new DeviceModel
                            {
                                Name = "A Dimmer Switch that is on",
                                Type = DeviceType.Dimmable,
                                Power = 75
                            },
                        new DeviceModel
                            {
                                Name = "A Dimmer Switch that is off",
                                Type = DeviceType.Dimmable,
                                Power = 0
                            },
                        thermostat,
                        new DeviceModel
                            {
                                Name = "A Thermostat with no data",
                                Type = DeviceType.Thermostat
                            }
                    };

                var computer = new ComputerModel
                    {
                        LastPing = DateTime.UtcNow
                    };

                var network = new NetworkModel
                    {
                        Name = "Example Network",
                        LastPing = DateTime.UtcNow,
                        AttatchedComputer = computer
                    };

                network.Devices = devices;

                foreach (var device in devices)
                {
                    device.IsConnected = true;
                    device.Network = network;
                }

                return devices;
            }
        }
    }
}
