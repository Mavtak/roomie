﻿using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class NetworkGuestRepository : INetworkGuestRepository
    {
        private readonly DbSet<EntityFrameworkNetworkGuestModel> _entries;
        private readonly DbSet<EntityFrameworkNetworkModel> _networks;
        private readonly DbSet<EntityFrameworkUserModel> _users;

        public NetworkGuestRepository(DbSet<EntityFrameworkNetworkGuestModel> entries, DbSet<EntityFrameworkNetworkModel> networks, DbSet<EntityFrameworkUserModel> users)
        {
            _entries = entries;
            _networks = networks;
            _users = users;
        }

        public Network[] Get(User user)
        {
            var matches = _entries.Where(x => x.User.Id == user.Id);
            var networks = matches.Select(x => x.Network);
            var result = networks
                .ToArray()
                .Select(x => x.ToRepositoryType())
                .ToArray();

            return result;
        }

        public User[] Get(Network network)
        {
            var matches = _entries.Where(x => x.Network.Id == network.Id);
            var users = matches.Select(x => x.User).Select(x => x.ToRepositoryType());
            var result = users.ToArray();

            return result;
        }

        public void Add(Network network, User user)
        {
            var entry = new EntityFrameworkNetworkGuestModel
            {
                Network = _networks.Find(network.Id),
                User = _users.Find(user.Id)
            };

            _entries.Add(entry);
        }

        public void Remove(Network network, User user)
        {
            var entry = Get(network, user);

            _entries.Remove(entry);
        }

        public bool Check(Network network, User user)
        {
            var entry = Get(network, user);
            var result = entry != null;

            return result;
        }

        private EntityFrameworkNetworkGuestModel Get(Network network, User user)
        {
            var result = _entries.Where(x => x.User.Id == user.Id)
                                 .Where(x => x.Network.Id == network.Id)
                                 .FirstOrDefault();

            return result;
        }
    }
}
