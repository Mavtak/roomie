using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Helpers
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
