using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class NetworkGuestRepository : INetworkGuestRepository
    {
        private readonly DbSet<EntityFrameworkNetworkGuestModel> _entries;
        private readonly DbSet<EntityFrameworkUserModel> _users;

        public NetworkGuestRepository(DbSet<EntityFrameworkNetworkGuestModel> entries, DbSet<EntityFrameworkUserModel> users)
        {
            _entries = entries;
            _users = users;
        }

        public EntityFrameworkNetworkModel[] Get(User user)
        {
            var matches = _entries.Where(x => x.User.Id == user.Id);
            var networks = matches.Select(x => x.Network);
            var result = networks.ToArray();

            return result;
        }

        public User[] Get(EntityFrameworkNetworkModel network)
        {
            var matches = _entries.Where(x => x.Network.Id == network.Id);
            var users = matches.Select(x => x.User).Select(x => x.ToRepositoryType());
            var result = users.ToArray();

            return result;
        }

        public void Add(EntityFrameworkNetworkModel network, User user)
        {
            var entry = new EntityFrameworkNetworkGuestModel
            {
                Network = network,
                User = _users.Find(user.Id)
            };

            _entries.Add(entry);
        }

        public void Remove(EntityFrameworkNetworkModel network, User user)
        {
            var entry = Get(network, user);

            _entries.Remove(entry);
        }

        public bool Check(EntityFrameworkNetworkModel network, User user)
        {
            var entry = Get(network, user);
            var result = entry != null;

            return result;
        }

        private EntityFrameworkNetworkGuestModel Get(EntityFrameworkNetworkModel network, User user)
        {
            var result = _entries.Where(x => x.User.Id == user.Id)
                                 .Where(x => x.Network.Id == network.Id)
                                 .FirstOrDefault();

            return result;
        }
    }
}
