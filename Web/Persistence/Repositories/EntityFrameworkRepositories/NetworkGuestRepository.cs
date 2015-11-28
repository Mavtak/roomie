using System;
using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class NetworkGuestRepository : INetworkGuestRepository
    {
        private readonly DbSet<NetworkGuestModel> _entries;
        private readonly DbSet<NetworkModel> _networks;
        private readonly Action _save;
        private readonly DbSet<UserModel> _users;

        public NetworkGuestRepository(DbSet<NetworkGuestModel> entries, DbSet<NetworkModel> networks, Action save, DbSet<UserModel> users)
        {
            _entries = entries;
            _networks = networks;
            _save = save;
            _users = users;
        }

        public Network[] Get(User user)
        {
            var result = _entries
                .Where(x => x.User.Id == user.Id)
                .Select(x => x.Network)
                .ToArray()
                .Select(x => x.ToRepositoryType())
                .ToArray();

            return result;
        }

        public User[] Get(Network network)
        {
            var result = _entries
                .Where(x => x.Network.Id == network.Id)
                .Select(x => x.User)
                .ToArray()
                .Select(x => x.ToRepositoryType())
                .ToArray();

            return result;
        }

        public void Add(Network network, User user)
        {
            var entry = new NetworkGuestModel
            {
                Network = _networks.Find(network.Id),
                User = _users.Find(user.Id),
            };

            _entries.Add(entry);

            _save();
        }

        public void Remove(Network network, User user)
        {
            var entry = Get(network, user);

            _entries.Remove(entry);

            _save();
        }

        public bool Check(Network network, User user)
        {
            var entry = Get(network, user);
            var result = entry != null;

            return result;
        }

        private NetworkGuestModel Get(Network network, User user)
        {
            var result = _entries
                .Where(x => x.User.Id == user.Id)
                .Where(x => x.Network.Id == network.Id)
                .FirstOrDefault();

            return result;
        }
    }
}
