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
        public static IEnumerable<Device> Devices
        {
            get
            {
                var id = -1;

                var computer = Computer.Create("Example Computer", null, DateTime.UtcNow);

                var network = Network.Create("Example Network", null, "Example Network", DateTime.UtcNow, computer);

                var onToggleSwitch = Device.Create(null, true, "A Toggle Switch that is on", network, null, DeviceType.BinarySwitch);
                onToggleSwitch.BinarySwitch.Power = BinarySwitchPower.On;
                onToggleSwitch = Device.Create(id--, onToggleSwitch);

                var offToggleSwitch = Device.Create(null, true, "A Toggle Switch that is off", network, null, DeviceType.BinarySwitch);
                offToggleSwitch.BinarySwitch.Power = BinarySwitchPower.Off;
                offToggleSwitch.PowerSensor.Value = new WattsPower(0);
                offToggleSwitch = Device.Create(id--, offToggleSwitch);

                var idleDevice = Device.Create(null, true, "An appliance that is on, but idle", network, null, DeviceType.BinarySwitch, "Idle");
                idleDevice.BinarySwitch.Power = BinarySwitchPower.On;
                idleDevice.PowerSensor.Value = new WattsPower(5);
                idleDevice = Device.Create(id--, idleDevice);

                var runningDevice = Device.Create(null, true, "An appliance that is on and running", network, null, DeviceType.BinarySwitch, "Running");
                runningDevice.BinarySwitch.Power = BinarySwitchPower.On;
                runningDevice.PowerSensor.Value = new WattsPower(50);
                runningDevice = Device.Create(id--, runningDevice);

                var onDimmerSwitch = Device.Create(null, true, "A Dimmer Switch that is on", network, null, DeviceType.MultilevelSwitch);
                onDimmerSwitch.MultilevelSwitch.Power = 75;
                onDimmerSwitch.PowerSensor.Value = new WattsPower(25.2);
                onDimmerSwitch.PowerSensor.TimeStamp = DateTime.UtcNow.AddSeconds(-5);
                onDimmerSwitch = Device.Create(id--, onDimmerSwitch);

                var offDimmerSwitch = Device.Create(null, true, "A Dimmer Switch that is off", network, null, DeviceType.MultilevelSwitch);
                offDimmerSwitch.MultilevelSwitch.Power = 0;
                offDimmerSwitch = Device.Create(id--, offDimmerSwitch);

                var dimmableColorChangingLight = Device.Create(null, true, "A dimmable, color-changing light", network, null, DeviceType.MultilevelSwitch);
                dimmableColorChangingLight.MultilevelSwitch.Power = 50;
                dimmableColorChangingLight.ColorSwitch.Value = new NamedColor("Purple");
                dimmableColorChangingLight = Device.Create(id--, dimmableColorChangingLight);

                var openDoorSensor = Device.Create(null, true, "A Door Sensor that is open", network, null, DeviceType.BinarySensor);
                openDoorSensor.BinarySensor.Type = BinarySensorType.Door;
                openDoorSensor.BinarySensor.Value = true;
                openDoorSensor.BinarySensor.TimeStamp = DateTime.UtcNow.AddSeconds(-24);
                openDoorSensor = Device.Create(id--, openDoorSensor);

                var stillMotionSensor = Device.Create(null, true, "A Motion Sensor that is still", network, null, DeviceType.BinarySensor);
                stillMotionSensor.BinarySensor.Type = BinarySensorType.Motion;
                stillMotionSensor.BinarySensor.Value = false;
                stillMotionSensor = Device.Create(id--, stillMotionSensor);

                var falseGenericBinarySensor = Device.Create(null, true, "A generic Binary Sensor that is false", network, null, DeviceType.BinarySensor);
                falseGenericBinarySensor.BinarySensor.Value = false;
                falseGenericBinarySensor.BinarySensor.TimeStamp = DateTime.UtcNow.AddMinutes(-4);
                falseGenericBinarySensor = Device.Create(id--, falseGenericBinarySensor);


                var multisensor = Device.Create(null, true, "A Multisensor", network, null, DeviceType.BinarySensor);
                multisensor.TemperatureSensor.Value = new CelsiusTemperature(25);
                multisensor.TemperatureSensor.TimeStamp = DateTime.UtcNow.AddSeconds(-45);
                multisensor.HumiditySensor.Value = new RelativeHumidity(35);
                multisensor.HumiditySensor.TimeStamp = DateTime.UtcNow.AddMinutes(-3).AddSeconds(-14);
                multisensor.IlluminanceSensor.Value = new LuxIlluminance(234);
                multisensor.IlluminanceSensor.TimeStamp = DateTime.UtcNow.AddMinutes(-36).AddSeconds(-14);
                multisensor.BinarySensor.Type = BinarySensorType.Motion;
                multisensor.BinarySensor.Value = true;
                multisensor.BinarySensor.TimeStamp = DateTime.UtcNow.AddSeconds(-43);
                multisensor = Device.Create(id--, multisensor);

                var thermostat = Device.Create(null, true, "A Thermostat with all data", network, null, DeviceType.Thermostat);
                thermostat.TemperatureSensor.Value = new FahrenheitTemperature(75);
                thermostat.Thermostat.Core.Mode = ThermostatMode.Auto;
                thermostat.Thermostat.Core.SupportedModes = new[] { ThermostatMode.Heat, ThermostatMode.Cool, ThermostatMode.Auto, ThermostatMode.FanOnly, ThermostatMode.Off };
                thermostat.Thermostat.Core.CurrentAction = ThermostatCurrentAction.Cooling;
                thermostat.Thermostat.Fan.Mode = ThermostatFanMode.Auto;
                thermostat.Thermostat.Fan.SupportedModes = new[] { ThermostatFanMode.Auto, ThermostatFanMode.On, };
                thermostat.Thermostat.Fan.CurrentAction = ThermostatFanCurrentAction.On;
                thermostat.Thermostat.Setpoints.Add(ThermostatSetpointType.Cool, new FahrenheitTemperature(74));
                thermostat.Thermostat.Setpoints.Add(ThermostatSetpointType.Heat, new FahrenheitTemperature(70));
                thermostat = Device.Create(id--, thermostat);

                var noDataThermostat = Device.Create(null, true, "A Thermostat with no data", network, null, DeviceType.Thermostat);
                noDataThermostat = Device.Create(id--, noDataThermostat);

                var devices = new[] { onToggleSwitch, offToggleSwitch, idleDevice, runningDevice, onDimmerSwitch, offDimmerSwitch, dimmableColorChangingLight, openDoorSensor, stillMotionSensor, falseGenericBinarySensor, multisensor, thermostat, noDataThermostat };

                return devices;
            }
        }
    }
}
