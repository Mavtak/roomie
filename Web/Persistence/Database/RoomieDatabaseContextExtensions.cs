using System.Linq;
using Roomie.Web.Persistence.Helpers;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Database
{
    public static class RoomieDatabaseContextExtensions
    {
        public static EntityFrameworkDeviceModel[] GetDevicesForUser(this IRoomieDatabaseContext database, EntityFrameworkUserModel user)
        {
            var networks = database.Networks.Get(user);
            var devices = networks.SelectMany(database.Devices.Get).ToList();

            devices.Sort(new DeviceSort());

            var result = devices.ToArray();

            return result;
        }
    }
}
