using System;
using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class NetworkRepository : INetworkRepository
    {
        private readonly DbSet<EntityFrameworkNetworkModel> _networks;

        public NetworkRepository(DbSet<EntityFrameworkNetworkModel> networks)
        {
            _networks = networks;
        }

        public EntityFrameworkNetworkModel Get(int id)
        {
            var result = _networks.Find(id);

            return result;
        }

        public EntityFrameworkNetworkModel Get(EntityFrameworkUserModel user, int id)
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

        public EntityFrameworkNetworkModel[] Get(EntityFrameworkUserModel user)
        {
            var results = _networks.Where(x => x.Owner.Id == user.Id).ToArray();

            return results;
        }

        public EntityFrameworkNetworkModel Get(EntityFrameworkUserModel user, string address)
        {
            var result = _networks.Where(x => x.Owner.Id == user.Id)
                                  .Where(x => x.Address == address)
                                  .FirstOrDefault();
            return result;
        }

        public void Add(EntityFrameworkNetworkModel network)
        {
            _networks.Add(network);
        }

        public void Remove(EntityFrameworkNetworkModel network)
        {
            _networks.Remove(network);
        }
    }
}
