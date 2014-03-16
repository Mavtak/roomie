using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public class EntityFrameworkDeviceRepository : IDeviceRepository
    {
        private readonly IRoomieEntitySet<DeviceModel> _devices;

        public EntityFrameworkDeviceRepository(IRoomieEntitySet<DeviceModel> devices)
        {
            _devices = devices;
        }

        public DeviceModel Get(int id)
        {
            var result = _devices.Find(id);

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
