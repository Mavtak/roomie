using System;
using Roomie.Common.HomeAutomation;
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

        public EntityFrameworkDeviceModel Get(int id)
        {
            var result = _devices.Get(id);

            return result;
        }

        public EntityFrameworkDeviceModel Get(User user, int id)
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

        public EntityFrameworkDeviceModel[] Get(EntityFrameworkNetworkModel network)
        {
            var result = _devices.Get(network);

            return result;
        }

        public void Add(EntityFrameworkDeviceModel device)
        {
            _devices.Add(device);
        }

        public void Remove(EntityFrameworkDeviceModel device)
        {
            _devices.Remove(device);
        }

        public void Update(int id, IDeviceState state)
        {
            _devices.Update(id, state);
        }
    }
}
