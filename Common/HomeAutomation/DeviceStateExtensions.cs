﻿using System;
using System.Linq;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.Measurements;

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
            else if (!device.Type.CanControl || !device.Type.CanPoll)
            {
                result = "n/a";
            }

            if (result == null)
            {
                result = "?";
            }

            if (device.IsConnected != true)
            {
                result += "?";
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
                var toggleSwitchNode = state.BinarySwitchState.ToXElement();

                if (toggleSwitchNode.Attributes().Any() || toggleSwitchNode.Descendants().Any())
                {
                    result.Add(toggleSwitchNode);
                }
            }

            if (state.MultilevelSwitchState != null)
            {
                var dimmerSwitchNode = state.MultilevelSwitchState.ToXElement();

                if (dimmerSwitchNode.Attributes().Any() || dimmerSwitchNode.Descendants().Any())
                {
                    result.Add(dimmerSwitchNode);
                }
            }

            if (state.BinarySensorState != null)
            {
                var binarySensorNode = state.BinarySensorState.ToXElement();

                if (binarySensorNode.Attributes().Any() || binarySensorNode.Descendants().Any())
                {
                    result.Add(binarySensorNode);
                }
            }

            if (state.PowerSensorState != null)
            {
                var powerSensorNode = state.PowerSensorState.ToXElement("PowerSensor");

                if (powerSensorNode.Attributes().Any() || powerSensorNode.Descendants().Any())
                {
                    result.Add(powerSensorNode);
                }
            }

            if (state.TemperatureSensorState != null)
            {
                var temperatureSensorNode = state.TemperatureSensorState.ToXElement("TemperatureSensor");

                if (temperatureSensorNode.Attributes().Any() || temperatureSensorNode.Descendants().Any())
                {
                    result.Add(temperatureSensorNode);
                }
            }

            if (state.HumiditySensorState != null)
            {
                var humiditySensorNode = state.HumiditySensorState.ToXElement("HumiditySensor");

                if (humiditySensorNode.Attributes().Any() || humiditySensorNode.Descendants().Any())
                {
                    result.Add(humiditySensorNode);
                }
            }

            if (state.IlluminanceSensorState != null)
            {
                var illuminanceSensorNode = state.IlluminanceSensorState.ToXElement("IlluminanceSensor");

                if (illuminanceSensorNode.Attributes().Any() || illuminanceSensorNode.Descendants().Any())
                {
                    result.Add(illuminanceSensorNode);
                }
            }

            if (state.ThermostatState != null)
            {
                var thermostatNode = state.ThermostatState.ToXElement();

                if (thermostatNode.Attributes().Any() || thermostatNode.Descendants().Any())
                {
                    result.Add(thermostatNode);
                }
            }

            if (state.KeypadState != null)
            {
                var node = state.KeypadState.ToXElement();

                if (node.Attributes().Any() || node.Descendants().Any())
                {
                    result.Add(node);
                }
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
