using System;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.ColorSwitch;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;

namespace Roomie.Common.HomeAutomation
{
    public static class DeviceStateExtensions
    {
        public static string Describe(this IDeviceState device)
        {
            string result = null;

            if (device.Type == DeviceType.BinarySwitch)
            {
                result = device.BinarySwitchState.Describe();
            }
            else if (device.Type == DeviceType.MultilevelSwitch)
            {
                result = device.MultilevelSwitchState.Describe();
            }
            else if (device.Type == DeviceType.BinarySensor)
            {
                result = device.BinarySensorState.Describe();
            }
            else if (device.Type == DeviceType.Thermostat)
            {
                result = device.ThermostatState.Describe();
            }
            else if (device.Type == DeviceType.Keypad)
            {
                result = device.KeypadState.Describe();
            }
            else if (device.Type == DeviceType.Unknown)
            {
                result = "?";
            }
            else
            {
                result = "n/a";
            }

            if (device.IsConnected != true)
            {
                result += "?";
            }

            if (device.CurrentAction != null)
            {
                result += ", Current Action = " + device.CurrentAction;
            }

            if (device.ColorSwitchState != null && device.ColorSwitchState.Value != null)
            {
                result += " " + device.ColorSwitchState.Describe();
            }

            if (device.Type != DeviceType.BinarySensor && (device.BinarySensorState != null && device.BinarySensorState.Value != null))
            {
                result = device.BinarySensorState.Describe();
            }

            if (device.PowerSensorState != null && device.PowerSensorState.Value != null)
            {
                result += " " + device.PowerSensorState.Describe();
            }

            if (device.TemperatureSensorState != null && device.TemperatureSensorState.Value != null)
            {
                result += " " + device.TemperatureSensorState.Describe();
            }

            if (device.HumiditySensorState != null && device.HumiditySensorState.Value != null)
            {
                result += " " + device.HumiditySensorState.Describe();
            }

            if (device.IlluminanceSensorState != null && device.IlluminanceSensorState.Value != null)
            {
                result += " " + device.IlluminanceSensorState.Describe();
            }

            return result;
        }

        public static ReadOnlyDeviceState Copy(this IDeviceState state)
        {
            return ReadOnlyDeviceState.CopyFrom(state);
        }

        public static XElement ToXElement(this IDeviceState state, string nodeName = "HomeAutomationDevice")
        {
            var result = new XElement(nodeName);

            if (!String.IsNullOrWhiteSpace(state.Address))
                result.Add(new XAttribute("Address", state.Address));

            if (!String.IsNullOrWhiteSpace(state.Name))
                result.Add(new XAttribute("Name", state.Name));

            if (state.Location != null && state.Location.IsSet)
                result.Add(new XAttribute("Location", state.Location.Format()));

            result.Add(new XAttribute("Type", state.Type));

            if (state.CurrentAction != null)
            {
                result.Add(new XAttribute("CurrentAction", state.CurrentAction));
            }
            //TODO: LastPoll

            //TODO: this is included for compatibility.  remove it soon
            if (state.MultilevelSwitchState != null && state.MultilevelSwitchState.Power != null)
            {
                result.Add(new XAttribute("Power", state.MultilevelSwitchState.Power.Value));
            }

            if (state.IsConnected != null)
            {
                result.Add(new XAttribute("IsConnected", state.IsConnected));
            }

            if (state.BinarySwitchState != null)
            {
                var element = state.BinarySwitchState.ToXElement();

                result.AddIfHasData(element);
            }

            if (state.MultilevelSwitchState != null)
            {
                var element = state.MultilevelSwitchState.ToXElement();

                result.AddIfHasData(element);
            }

            if (state.ColorSwitchState != null)
            {
                var element = state.ColorSwitchState.ToXElement();

                result.AddIfHasData(element);
            }

            if (state.BinarySensorState != null)
            {
                var element = state.BinarySensorState.ToXElement();

                result.AddIfHasData(element);
            }

            if (state.PowerSensorState != null)
            {
                var element = state.PowerSensorState.ToXElement("PowerSensor");

                result.AddIfHasData(element);
            }

            if (state.TemperatureSensorState != null)
            {
                var element = state.TemperatureSensorState.ToXElement("TemperatureSensor");

                result.AddIfHasData(element);
            }

            if (state.HumiditySensorState != null)
            {
                var element = state.HumiditySensorState.ToXElement("HumiditySensor");

                result.AddIfHasData(element);
            }

            if (state.IlluminanceSensorState != null)
            {
                var element = state.IlluminanceSensorState.ToXElement("IlluminanceSensor");

                result.AddIfHasData(element);
            }

            if (state.ThermostatState != null)
            {
                var element = state.ThermostatState.ToXElement();

                result.AddIfHasData(element);
            }

            if (state.KeypadState != null)
            {
                var element = state.KeypadState.ToXElement();

                result.AddIfHasData(element);
            }

            return result;
        }

        public static ReadOnlyDeviceState ToDeviceState(this XElement element)
        {
            return ReadOnlyDeviceState.FromXElement(element);
        }

        public static string BuildVirtualAddress(this IDeviceState state, bool justAddress, bool includeDescription)
        {
            string description = null;
            if (includeDescription)
            {
                description = VirtualAddress.FormatNaturalLanguageDescription(state);
            }

            return VirtualAddress.Format(state, justAddress, description);
        }
    }
}
