using System.Linq;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Helpers;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public class EntityFrameworkDeviceRepository : IDeviceRepository
    {
        private readonly IRoomieEntitySet<DeviceModel> _devices;
        private readonly INetworkRepository _networks;

        public EntityFrameworkDeviceRepository(IRoomieEntitySet<DeviceModel> devices, INetworkRepository networks)
        {
            _devices = devices;
            _networks = networks;
        }

        public DeviceModel Get(int id)
        {
            var result = _devices.Find(id);

            return result;
        }

        public DeviceModel[] Get(NetworkModel network)
        {
            var result = _devices.Where(x => x.Network.Id == network.Id).ToArray();

            return result;
        }

        public DeviceModel[] Get(UserModel user)
        {
            var networks = _networks.Get(user);
            var devices = networks.SelectMany(Get).ToList();

            devices.Sort(new DeviceSort());

            var result = devices.ToArray();

            return result;
        }

        public void Add(DeviceModel device)
        {
            _devices.Add(device);
        }

        public void Remove(DeviceModel device)
        {
            _devices.Remove(device);
        }
    }
}
