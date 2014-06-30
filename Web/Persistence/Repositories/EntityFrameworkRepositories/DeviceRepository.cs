using System;
using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DbSet<DeviceModel> _devices;

        public DeviceRepository(DbSet<DeviceModel> devices)
        {
            _devices = devices;
        }

        public DeviceModel Get(int id)
        {
            var result = _devices.Find(id);

            return result;
        }

        public DeviceModel Get(UserModel user, int id)
        {
            var result = Get(id);

            if (result == null)
            {
                return null;
            }

            if (result.Network == null)
            {
                throw new Exception("Network not set");
            }

            if (result.Network.Owner == null)
            {
                throw new Exception("Owner not set");
            }

            if (result.Network.Owner.Id != user.Id)
            {
                result = null;
            }

            return result;
        }

        public DeviceModel[] Get(NetworkModel network)
        {
            var result = _devices.Where(x => x.Network.Id == network.Id).ToArray();

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
