using System;
using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class NetworkRepository : INetworkRepository
    {
        private readonly DbSet<NetworkModel> _networks;
        private readonly DbSet<ComputerModel> _computers;
        private readonly DbSet<UserModel> _users;

        public NetworkRepository(DbSet<NetworkModel> networks, DbSet<ComputerModel> computers, DbSet<UserModel> users)
        {
            _networks = networks;
            _computers = computers;
            _users = users;
        }

        public Network Get(int id)
        {
            var result = _networks.Find(id);

            if (result == null)
            {
                return null;
            }

            return result.ToRepositoryType();
        }

        public Network Get(User user, int id)
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

        public Network[] Get(User user)
        {
            var results = _networks
                .Where(x => x.Owner.Id == user.Id)
                .ToArray()
                .Select(x => x.ToRepositoryType())
                .ToArray();

            return results;
        }

        public Network Get(User user, string address)
        {
            var result = _networks.Where(x => x.Owner.Id == user.Id)
                                  .Where(x => x.Address == address)
                                  .FirstOrDefault();

            if (result == null)
            {
                return null;
            }

            return result.ToRepositoryType();
        }

        public void Add(Network network)
        {
            var model = NetworkModel.FromRepositoryType(network, _computers, _users);

            _networks.Add(model);
        }

        public void Update(Network network)
        {
            var model = _networks.Find(network.Id);

            model.Address = network.Address;
            model.AttatchedComputer = _computers.Find(network.AttatchedComputer.Id);
            model.LastPing = network.LastPing;
            model.Name = network.Name;
        }

        public void Remove(Network network)
        {
            var model = _networks.Find(network.Id);

            _networks.Remove(model);
        }
    }
}
