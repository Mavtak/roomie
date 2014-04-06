using System;
using System.Collections.Generic;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Measurements.Power;
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
                        Type = DeviceType.BinarySwitch
                    };
                onToggleSwitch.ToggleSwitch.Power = BinarySwitchPower.On;

                var offToggleSwitch = new DeviceModel
                    {
                        Name = "A Toggle Switch that is off",
                        Type = DeviceType.BinarySwitch,
                    };
                offToggleSwitch.ToggleSwitch.Power = BinarySwitchPower.Off;
                offToggleSwitch.PowerSensor.Value = new WattsPower(0);

                var onDimmerSwitch = new DeviceModel
                    {
                        Name = "A Dimmer Switch that is on",
                        Type = DeviceType.MultilevelSwitch
                    };
                onDimmerSwitch.DimmerSwitch.Power = 75;
                onDimmerSwitch.PowerSensor.Value = new WattsPower(25.2);

                var offDimmerSwitch = new DeviceModel
                    {
                        Name = "A Dimmer Switch that is off",
                        Type = DeviceType.MultilevelSwitch
                    };
                offDimmerSwitch.DimmerSwitch.Power = 0;


                var openDoorSensor = new DeviceModel
                {
                    Name = "A Door Sensor that is open",
                    Type = DeviceType.BinarySensor
                };
                openDoorSensor.BinarySensor.Type = BinarySensorType.Door;
                openDoorSensor.BinarySensor.Value = true;

                var stillMotionSensor = new DeviceModel
                {
                    Name = "A Motion Sensor that is still",
                    Type = DeviceType.BinarySensor
                };
                stillMotionSensor.BinarySensor.Type = BinarySensorType.Motion;
                stillMotionSensor.BinarySensor.Value = false;

                var falseGenericBinarySensor = new DeviceModel
                {
                    Name = "A generic Binary Sensor that is false",
                    Type = DeviceType.BinarySensor
                };
                falseGenericBinarySensor.BinarySensor.Value = false;

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

                var devices = new[] { onToggleSwitch, offToggleSwitch, onDimmerSwitch, offDimmerSwitch, openDoorSensor, stillMotionSensor, falseGenericBinarySensor, thermostat, noDataThermostat };

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
