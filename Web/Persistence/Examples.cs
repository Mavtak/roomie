using System;
using System.Collections.Generic;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.HomeAutomation.ToggleSwitches;
using Roomie.Common.Measurements.Temperature;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence
{
    public static class Examples
    {
        public static IEnumerable<DeviceModel> Devices
        {
            get
            {
                var onToggleSwitch = new DeviceModel
                    {
                        Name = "A Toggle Switch that is on",
                        Type = DeviceType.Switch
                    };
                onToggleSwitch.ToggleSwitch.Power = BinarySwitchPower.On;

                var offToggleSwitch = new DeviceModel
                    {
                        Name = "A Toggle Switch that is off",
                        Type = DeviceType.Switch
                    };
                offToggleSwitch.ToggleSwitch.Power = BinarySwitchPower.Off;

                var onDimmerSwitch = new DeviceModel
                    {
                        Name = "A Dimmer Switch that is on",
                        Type = DeviceType.Dimmable
                    };
                onDimmerSwitch.DimmerSwitch.Power = 75;

                var offDimmerSwitch = new DeviceModel
                    {
                        Name = "A Dimmer Switch that is off",
                        Type = DeviceType.Dimmable
                    };
                offDimmerSwitch.DimmerSwitch.Power = 0;

                var thermostat = new DeviceModel
                    {
                        Name = "A Thermostat with all data",
                        Type = DeviceType.Thermostat,
                    };
                thermostat.Thermostat.Temperature = new FahrenheitTemperature(75);
                thermostat.Thermostat.Core.Mode = ThermostatMode.Auto;
                thermostat.Thermostat.Core.SupportedModes = new[] { ThermostatMode.Heat, ThermostatMode.Cool, ThermostatMode.Auto, ThermostatMode.FanOnly, ThermostatMode.Off };
                thermostat.Thermostat.Core.CurrentAction = ThermostatCurrentAction.Cooling;
                thermostat.Thermostat.Fan.Mode = ThermostatFanMode.Auto;
                thermostat.Thermostat.Fan.SupportedModes = new[] { ThermostatFanMode.Auto, ThermostatFanMode.On, };
                thermostat.Thermostat.Fan.CurrentAction = ThermostatFanCurrentAction.On;
                thermostat.Thermostat.Setpoints.Add(ThermostatSetpointType.Cool, new FahrenheitTemperature(74));
                thermostat.Thermostat.Setpoints.Add(ThermostatSetpointType.Heat, new FahrenheitTemperature(70));

                var noDataThermostat = new DeviceModel
                    {
                        Name = "A Thermostat with no data",
                        Type = DeviceType.Thermostat
                    };

                var devices = new[] { onToggleSwitch, offToggleSwitch, onDimmerSwitch, offDimmerSwitch, thermostat, noDataThermostat };

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
