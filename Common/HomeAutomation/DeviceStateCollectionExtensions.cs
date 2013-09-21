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
    }
}
