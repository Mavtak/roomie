using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public class EntityFrameworkNetworkGuestRepository : INetworkGuestRepository
    {
        private readonly DbSet<NetworkGuestModel> _entries;

        public EntityFrameworkNetworkGuestRepository(DbSet<NetworkGuestModel> entries)
        {
            _entries = entries;
        }

        public NetworkModel[] Get(UserModel user)
        {
            var matches = _entries.Where(x => x.User.Id == user.Id);
            var networks = matches.Select(x => x.Network);
            var result = networks.ToArray();

            return result;
        }

        public UserModel[] Get(NetworkModel network)
        {
            var matches = _entries.Where(x => x.Network.Id == network.Id);
            var users = matches.Select(x => x.User);
            var result = users.ToArray();

            return result;
        }

        public void Add(NetworkModel network, UserModel user)
        {
            var entry = new NetworkGuestModel
            {
                Network = network,
                User = user
            };

            _entries.Add(entry);
        }

        public void Remove(NetworkModel network, UserModel user)
        {
            var entry = Get(network, user);

            _entries.Remove(entry);
        }

        public bool Check(NetworkModel network, UserModel user)
        {
            var entry = Get(network, user);
            var result = entry != null;

            return result;
        }

        private NetworkGuestModel Get(NetworkModel network, UserModel user)
        {
            var result = _entries.Where(x => x.User.Id == user.Id)
                                 .Where(x => x.Network.Id == network.Id)
                                 .FirstOrDefault();

            return result;
        }
    }
}
