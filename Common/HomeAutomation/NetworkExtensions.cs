using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation
{
    public static class NetworkExtensions
    {
        public static XElement ToXElement(this INetworkState state, string nodeName = "HomeAutomationNetwork")
        {
            var result = new XElement(nodeName);

            if (!String.IsNullOrWhiteSpace(state.Address))
            {
                result.Add(new XAttribute("Address", state.Address));
            }

            if (!String.IsNullOrWhiteSpace(state.Name))
            {
                result.Add(new XAttribute("Name", state.Name));
            }

            foreach (var device in state.DeviceStates)
            {
                result.Add(device.ToXElement());
            }

            return result;
        }

        public static ReadOnlyNetworkState ToNetworkState(this XElement element)
        {
            return ReadOnlyNetworkState.FromXElement(element);
        }
    }
}
