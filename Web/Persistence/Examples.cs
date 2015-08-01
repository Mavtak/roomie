using System;
using System.Collections.Generic;
using Roomie.Common.Color;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Measurements.Humidity;
using Roomie.Common.Measurements.Illuminance;
using Roomie.Common.Measurements.Power;
using Roomie.Common.Measurements.Temperature;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence
{
    public static class Examples
    {
        public static IEnumerable<EntityFrameworkDeviceModel> Devices
        {
            get
            {
                var onToggleSwitch = new EntityFrameworkDeviceModel
                    {
                        Name = "A Toggle Switch that is on",
                        Type = DeviceType.BinarySwitch
                    };
                onToggleSwitch.BinarySwitch.Power = BinarySwitchPower.On;

                var offToggleSwitch = new EntityFrameworkDeviceModel
                    {
                        Name = "A Toggle Switch that is off",
                        Type = DeviceType.BinarySwitch,
                    };
                offToggleSwitch.BinarySwitch.Power = BinarySwitchPower.Off;
                offToggleSwitch.PowerSensor.Value = new WattsPower(0);

                var idleDevice = new EntityFrameworkDeviceModel
                    {
                        Name = "An appliance that is on, but idle",
                        Type = DeviceType.BinarySwitch,
                    };
                idleDevice.BinarySwitch.Power = BinarySwitchPower.On;
                idleDevice.PowerSensor.Value = new WattsPower(5);
                idleDevice.CurrentAction = "Idle";

                var runningDevice = new EntityFrameworkDeviceModel
                    {
                        Name = "An appliance that is on and running",
                        Type = DeviceType.BinarySwitch,
                    };
                runningDevice.BinarySwitch.Power = BinarySwitchPower.On;
                runningDevice.PowerSensor.Value = new WattsPower(50);
                runningDevice.CurrentAction = "Running";


                var onDimmerSwitch = new EntityFrameworkDeviceModel
                    {
                        Name = "A Dimmer Switch that is on",
                        Type = DeviceType.MultilevelSwitch
                    };
                onDimmerSwitch.MultilevelSwitch.Power = 75;
                onDimmerSwitch.PowerSensor.Value = new WattsPower(25.2);
                onDimmerSwitch.PowerSensor.TimeStamp = DateTime.UtcNow.AddSeconds(-5);

                var offDimmerSwitch = new EntityFrameworkDeviceModel
                    {
                        Name = "A Dimmer Switch that is off",
                        Type = DeviceType.MultilevelSwitch
                    };
                offDimmerSwitch.MultilevelSwitch.Power = 0;

                var dimmableColorChangingLight = new EntityFrameworkDeviceModel
                    {
                        Name = "A dimmable, color-changing light",
                        Type = DeviceType.MultilevelSwitch
                    };
                dimmableColorChangingLight.MultilevelSwitch.Power = 50;
                dimmableColorChangingLight.ColorSwitch.Value = new NamedColor("Purple");

                var openDoorSensor = new EntityFrameworkDeviceModel
                {
                    Name = "A Door Sensor that is open",
                    Type = DeviceType.BinarySensor
                };
                openDoorSensor.BinarySensor.Type = BinarySensorType.Door;
                openDoorSensor.BinarySensor.Value = true;
                openDoorSensor.BinarySensor.TimeStamp = DateTime.UtcNow.AddSeconds(-24);

                var stillMotionSensor = new EntityFrameworkDeviceModel
                {
                    Name = "A Motion Sensor that is still",
                    Type = DeviceType.BinarySensor
                };
                stillMotionSensor.BinarySensor.Type = BinarySensorType.Motion;
                stillMotionSensor.BinarySensor.Value = false;

                var falseGenericBinarySensor = new EntityFrameworkDeviceModel
                {
                    Name = "A generic Binary Sensor that is false",
                    Type = DeviceType.BinarySensor
                };
                falseGenericBinarySensor.BinarySensor.Value = false;
                falseGenericBinarySensor.BinarySensor.TimeStamp = DateTime.UtcNow.AddMinutes(-4);


                var multisensor = new EntityFrameworkDeviceModel
                {
                    Name = "A Multisensor",
                    Type = DeviceType.BinarySensor
                };
                multisensor.TemperatureSensor.Value = new CelsiusTemperature(25);
                multisensor.TemperatureSensor.TimeStamp = DateTime.UtcNow.AddSeconds(-45);
                multisensor.HumiditySensor.Value = new RelativeHumidity(35);
                multisensor.HumiditySensor.TimeStamp = DateTime.UtcNow.AddMinutes(-3).AddSeconds(-14);
                multisensor.IlluminanceSensor.Value = new LuxIlluminance(234);
                multisensor.IlluminanceSensor.TimeStamp = DateTime.UtcNow.AddMinutes(-36).AddSeconds(-14);
                multisensor.BinarySensor.Type = BinarySensorType.Motion;
                multisensor.BinarySensor.Value = true;
                multisensor.BinarySensor.TimeStamp = DateTime.UtcNow.AddSeconds(-43);

                var thermostat = new EntityFrameworkDeviceModel
                    {
                        Name = "A Thermostat with all data",
                        Type = DeviceType.Thermostat,
                    };
                thermostat.TemperatureSensor.Value = new FahrenheitTemperature(75);
                thermostat.Thermostat.Core.Mode = ThermostatMode.Auto;
                thermostat.Thermostat.Core.SupportedModes = new[] { ThermostatMode.Heat, ThermostatMode.Cool, ThermostatMode.Auto, ThermostatMode.FanOnly, ThermostatMode.Off };
                thermostat.Thermostat.Core.CurrentAction = ThermostatCurrentAction.Cooling;
                thermostat.Thermostat.Fan.Mode = ThermostatFanMode.Auto;
                thermostat.Thermostat.Fan.SupportedModes = new[] { ThermostatFanMode.Auto, ThermostatFanMode.On, };
                thermostat.Thermostat.Fan.CurrentAction = ThermostatFanCurrentAction.On;
                thermostat.Thermostat.Setpoints.Add(ThermostatSetpointType.Cool, new FahrenheitTemperature(74));
                thermostat.Thermostat.Setpoints.Add(ThermostatSetpointType.Heat, new FahrenheitTemperature(70));

                var noDataThermostat = new EntityFrameworkDeviceModel
                    {
                        Name = "A Thermostat with no data",
                        Type = DeviceType.Thermostat
                    };

                var devices = new[] { onToggleSwitch, offToggleSwitch, idleDevice, runningDevice, onDimmerSwitch, offDimmerSwitch, dimmableColorChangingLight, openDoorSensor, stillMotionSensor, falseGenericBinarySensor, multisensor, thermostat, noDataThermostat };

                var computer = new EntityFrameworkComputerModel
                    {
                        LastPing = DateTime.UtcNow
                    };

                var network = new EntityFrameworkNetworkModel
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
