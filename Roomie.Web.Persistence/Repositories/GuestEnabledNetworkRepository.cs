using System;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public class GuestEnabledNetworkRepository : INetworkRepository
    {
        private readonly INetworkRepository _networks;
        private readonly INetworkGuestRepository _guests;

        public GuestEnabledNetworkRepository(INetworkRepository networks, INetworkGuestRepository guests)
        {
            _networks = networks;
            _guests = guests;
        }

        public Network Get(int id, IRepositoryModelCache cache = null)
        {
            var result = _networks.Get(id, cache);

            return result;
        }

        public Network Get(User user, int id, IRepositoryModelCache cache = null)
        {
            var result = Get(id, cache);

            if (result == null)
            {
                return null;
            }

            if (result.Owner == null)
            {
                throw new Exception("Owner not set");
            }

            if (result.Owner.Id != user.Id)
            {
                var guest = _guests.Check(result, user);

                if (!guest)
                {
                    result = null;
                }
            }

            return result;
        }

        public Network[] Get(User user, IRepositoryModelCache cache = null)
        {
            var primaryNetworks = _networks.Get(user, cache);
            var guestNetworks = _guests.Get(user);
            var result = primaryNetworks.Concat(guestNetworks).ToArray();

            return result;
        }

        public Network Get(User user, string address, IRepositoryModelCache cache = null)
        {
            var result = _networks.Get(user, address, cache);

            if (result == null)
            {
                var guestNetworks = _guests.Get(user);
                result = guestNetworks.FirstOrDefault(x => string.Equals(x, address));
            }

            return result;
        }

        public void Add(Network network)
        {
            _networks.Add(network);
        }

        public void Update(Network network)
        {
            _networks.Update(network);
        }

        public void Remove(Network network)
        {
            //TODO: improve this method
            _networks.Remove(network);

            foreach (var guest in _guests.Get(network))
            {
                _guests.Remove(network, guest);
            }
        }
    }
}
