using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class NetworkGuestRepository : INetworkGuestRepository
    {
        private readonly DbSet<EntityFrameworkNetworkGuestModel> _entries;

        public NetworkGuestRepository(DbSet<EntityFrameworkNetworkGuestModel> entries)
        {
            _entries = entries;
        }

        public EntityFrameworkNetworkModel[] Get(EntityFrameworkUserModel user)
        {
            var matches = _entries.Where(x => x.User.Id == user.Id);
            var networks = matches.Select(x => x.Network);
            var result = networks.ToArray();

            return result;
        }

        public EntityFrameworkUserModel[] Get(EntityFrameworkNetworkModel network)
        {
            var matches = _entries.Where(x => x.Network.Id == network.Id);
            var users = matches.Select(x => x.User);
            var result = users.ToArray();

            return result;
        }

        public void Add(EntityFrameworkNetworkModel network, EntityFrameworkUserModel user)
        {
            var entry = new EntityFrameworkNetworkGuestModel
            {
                Network = network,
                User = user
            };

            _entries.Add(entry);
        }

        public void Remove(EntityFrameworkNetworkModel network, EntityFrameworkUserModel user)
        {
            var entry = Get(network, user);

            _entries.Remove(entry);
        }

        public bool Check(EntityFrameworkNetworkModel network, EntityFrameworkUserModel user)
        {
            var entry = Get(network, user);
            var result = entry != null;

            return result;
        }

        private EntityFrameworkNetworkGuestModel Get(EntityFrameworkNetworkModel network, EntityFrameworkUserModel user)
        {
            var result = _entries.Where(x => x.User.Id == user.Id)
                                 .Where(x => x.Network.Id == network.Id)
                                 .FirstOrDefault();

            return result;
        }
    }
}
