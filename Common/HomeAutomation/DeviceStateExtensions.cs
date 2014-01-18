using System;
using System.Linq;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.Common.HomeAutomation
{
    public static class DeviceStateExtensions
    {
        public static string Describe(this IDeviceState device)
        {
            string result = null;

            if (device.Type == DeviceType.Switch || device.Type == DeviceType.MotionDetector)
            {
                //TODO: account for motion detectors specifically
                result = device.ToggleSwitchState.Describe();
            }
            else if (device.Type == DeviceType.Dimmable)
            {
                result = device.DimmerSwitchState.Describe();
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
            if (state.DimmerSwitchState != null && state.DimmerSwitchState.Power != null)
            {
                result.Add(new XAttribute("Power", state.DimmerSwitchState.Power.Value));
            }

            if (state.IsConnected != null)
            {
                result.Add(new XAttribute("IsConnected", state.IsConnected));
            }

            if (state.ToggleSwitchState != null)
            {
                var toggleSwitchNode = state.ToggleSwitchState.ToXElement();

                if (toggleSwitchNode.Attributes().Any() || toggleSwitchNode.Ancestors().Any())
                {
                    result.Add(toggleSwitchNode);
                }
            }

            if (state.DimmerSwitchState != null)
            {
                var dimmerSwitchNode = state.DimmerSwitchState.ToXElement();

                if (dimmerSwitchNode.Attributes().Any() || dimmerSwitchNode.Ancestors().Any())
                {
                    result.Add(dimmerSwitchNode);
                }
            }

            if (state.ThermostatState != null)
            {
                var thermostatNode = state.ThermostatState.ToXElement();

                if (thermostatNode.Attributes().Any() || thermostatNode.Ancestors().Any())
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
