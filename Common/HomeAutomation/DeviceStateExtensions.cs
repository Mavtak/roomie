using System;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation.DimmerSwitches;
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

            if (state.Location.IsSet)
                result.Add(new XAttribute("Location", state.Location.Name));

            result.Add(new XAttribute("Type", state.Type));
            //TODO: LastPoll

            //TODO: handle this better
            if (state.DimmerSwitchState.Power != null)
            {
                result.Add(new XAttribute("Power", state.DimmerSwitchState.Power.Value));
            }

            if (state.IsConnected != null)
            {
                result.Add(new XAttribute("IsConnected", state.IsConnected));
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
