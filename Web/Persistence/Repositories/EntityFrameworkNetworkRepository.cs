using System;
using System.Linq;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public class EntityFrameworkNetworkRepository : INetworkRepository
    {
        private readonly IRoomieEntitySet<NetworkModel> _networks;

        public EntityFrameworkNetworkRepository(IRoomieEntitySet<NetworkModel> networks)
        {
            _networks = networks;
        }

        public NetworkModel Get(int id)
        {
            var result = _networks.Find(id);

            return result;
        }

        public NetworkModel Get(UserModel user, int id)
        {
            var result = Get(id);

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
                result = null;
            }

            return result;
        }

        public NetworkModel[] Get(UserModel user)
        {
            var results = _networks.Where(x => x.Owner.Id == user.Id).ToArray();

            return results;
        }

        public NetworkModel Get(UserModel user, string address)
        {
            var result = _networks.Where(x => x.Owner.Id == user.Id)
                                  .Where(x => x.Address == address)
                                  .FirstOrDefault();
            return result;
        }

        public void Add(NetworkModel network)
        {
            _networks.Add(network);
        }

        public void Remove(NetworkModel network)
        {
            _networks.Remove(network);
        }
    }
}
