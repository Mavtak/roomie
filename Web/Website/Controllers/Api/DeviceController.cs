using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api
{
    [ApiRestrictedAccessAttribute]
    [AutoSave]
    public class DeviceController : RoomieBaseApiController
    {
        public IEnumerable<IDeviceState> Get()
        {
            var devices = Database.GetDevicesForUser(User);
            var result = devices.Select(GetSerializableVersion);

            return result;
        }

        public IDeviceState Get(int id)
        {
            var device = this.SelectDevice(id);
            var result = GetSerializableVersion(device);

            return result;
        }

        private static IDeviceState GetSerializableVersion(IDeviceState device)
        {
            var result = ReadOnlyDeviceState.CopyFrom(device)
                .NewWithNetwork(null);

            return result;
        }
    }
}
