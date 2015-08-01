using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface INetworkRepository
    {
        EntityFrameworkNetworkModel Get(int id);
        EntityFrameworkNetworkModel Get(EntityFrameworkUserModel user, int id);
        EntityFrameworkNetworkModel[] Get(EntityFrameworkUserModel user);
        EntityFrameworkNetworkModel Get(EntityFrameworkUserModel user, string address);
        void Add(EntityFrameworkNetworkModel network);
        void Remove(EntityFrameworkNetworkModel network);
    }
}
