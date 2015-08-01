using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface INetworkRepository
    {
        EntityFrameworkNetworkModel Get(int id);
        EntityFrameworkNetworkModel Get(User user, int id);
        EntityFrameworkNetworkModel[] Get(User user);
        EntityFrameworkNetworkModel Get(User user, string address);
        void Add(EntityFrameworkNetworkModel network);
        void Remove(EntityFrameworkNetworkModel network);
    }
}
