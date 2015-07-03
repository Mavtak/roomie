using System;
using System.Collections.Generic;
using System.Linq;
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
            var parts = new List<string>();

            if (device.Type == DeviceType.BinarySwitch ||
                (device.BinarySwitchState != null && device.BinarySwitchState.Power != null))
            {
                parts.Add(device.BinarySwitchState.Describe());
            }

            if (device.Type == DeviceType.MultilevelSwitch ||
                (device.MultilevelSwitchState != null && device.MultilevelSwitchState.Power != null))
            {
                parts.Add(device.MultilevelSwitchState.Describe());
            }
            
            if (device.Type == DeviceType.Thermostat)
            {
                parts.Add(device.ThermostatState.Describe());
            }
            
            if (device.Type == DeviceType.Keypad ||
                (device.KeypadState != null && device.KeypadState.Buttons != null && device.KeypadState.Buttons.Any()))
            {
                parts.Add(device.KeypadState.Describe());
            }

            if (device.CurrentAction != null)
            {
                parts.Add("Current Action = " + device.CurrentAction);
            }

            if (device.ColorSwitchState != null && device.ColorSwitchState.Value != null)
            {
                parts.Add(device.ColorSwitchState.Describe());
            }

            if(device.BinarySensorState != null && device.BinarySensorState.Value != null)
            {
                parts.Add(device.BinarySensorState.Describe());
            }

            if (device.PowerSensorState != null && device.PowerSensorState.Value != null)
            {
                parts.Add(device.PowerSensorState.Describe());
            }

            if (device.TemperatureSensorState != null && device.TemperatureSensorState.Value != null)
            {
                parts.Add(device.TemperatureSensorState.Describe());
            }

            if (device.HumiditySensorState != null && device.HumiditySensorState.Value != null)
            {
                parts.Add(device.HumiditySensorState.Describe());
            }

            if (device.IlluminanceSensorState != null && device.IlluminanceSensorState.Value != null)
            {
                parts.Add(device.IlluminanceSensorState.Describe());
            }

            var result = string.Join(", ", parts);

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
