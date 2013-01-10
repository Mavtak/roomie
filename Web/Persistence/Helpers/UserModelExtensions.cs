using System.Collections.Generic;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Helpers
{
    public static class UserModelExtensions
    {
        public static IEnumerable<DeviceModel> GetAllDevices(this UserModel user)
        {
            var devices = new List<DeviceModel>();

            foreach (var network in user.HomeAutomationNetworks)
            {
                foreach (DeviceModel device in network.Devices)
                {
                    devices.Add(device);
                }
            }

            devices.Sort(new DeviceSort());

            return devices;
        }
    }
}
