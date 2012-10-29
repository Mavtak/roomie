using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomie.Web.Models.Helpers
{
    public static class NetworkModelExtensions
    {
        public static void SortDevices(this NetworkModel network)
        {
            var devices = network.Devices.ToList();
            devices.Sort(new DeviceSort());
            network.Devices = devices;
        }
    }
}
