using System;
using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation
{
    public static class DeviceStateExtensions
    {
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
