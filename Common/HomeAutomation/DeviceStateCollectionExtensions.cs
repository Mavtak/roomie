using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.Common.HomeAutomation
{
    public static class DeviceStateCollectionExtensions
    {

        public static bool ContainsAddress(this IEnumerable<IDeviceState> deviceStates, string key)
        {
            return deviceStates.Any(device => device.Address == key);
        }

        public static bool ContainsName(this IEnumerable<IDeviceState> deviceStates, string key)
        {
            return deviceStates.Any(device => device.Name == key);
        }

        public static bool Contains(this IEnumerable<IDeviceState> deviceStates, string key)
        {
            return deviceStates.ContainsAddress(key) || deviceStates.ContainsAddress(key);
        }

        //TODO improve this
        public static T GetDevice<T>(this IEnumerable<T> deviceStates, string key)
            where T : IDeviceState
        {
            var result = from device in deviceStates
                         where device.Address == key
                         || device.Name == key
                         select device;

            var count = result.Count();
            if (count == 0)
            {
                throw new DeviceNotFoundException(key);
            }
            if (count > 1)
            {
                throw new MultipleMatchingDevicesException(key, result.Cast<IDeviceState>());
            }
            else
            {
                return result.First();
            }
        }

        //TODO: give this method a little more love
        public static T GetDevice<T>(this IEnumerable<T> deviceStates, VirtualAddress address)
            where T : IDeviceState
        {
            //TODO: select device by ID
            var results = (from d in deviceStates
                           where true
                               //TODO: add networkLocation
                                 && ((address.NetworkName == null) || d.NetworkState.Name == address.NetworkName)
                                 && ((address.NetworkNodeId == null) || d.NetworkState.Address == address.NetworkNodeId)
                                 && ((address.DeviceName == null) || (d.Name == address.DeviceName || d.Address == address.DeviceName))
                                 && ((address.DeviceNodeId == null) || d.Address == address.DeviceNodeId)
                           orderby LocationCloseness(d.Location, address.DeviceLocation) ascending
                           select d)
                      .ToList();


            if (!results.Any())
            {
                throw new NoMatchingDeviceException(address.Format());
            }

            var firstResult = results.First();
            results = results.Where(d => LocationCloseness(d.Location, address.DeviceLocation) == LocationCloseness(firstResult.Location, address.DeviceLocation)).ToList();

            if (results.Count() > 1)
            {
                throw new MultipleMatchingDevicesException(address.Format(), results.Cast<IDeviceState>());
            }

            return results.First();
        }

        private static int LocationCloseness(ILocation location1, string location2)
        {
            return location1.CalculateCloseness(new Location(location2));
        }
    }
}
