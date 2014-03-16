using System;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public class GuestEnabledDeviceRepository : IDeviceRepository
    {
        private readonly IDeviceRepository _devices;
        private readonly INetworkGuestRepository _guests;

        public GuestEnabledDeviceRepository(IDeviceRepository devices, INetworkGuestRepository guests)
        {
            _devices = devices;
            _guests = guests;
        }

        public DeviceModel Get(int id)
        {
            var result = _devices.Get(id);

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
                var guest = _guests.Check(result.Network, user);
                
                if (!guest)
                {
                    result = null;
                }
            }

            return result;
        }

        public DeviceModel[] Get(NetworkModel network)
        {
            var result = _devices.Get(network);

            return result;
        }

        public DeviceModel[] Get(UserModel user)
        {
            var result = _devices.Get(user);

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
