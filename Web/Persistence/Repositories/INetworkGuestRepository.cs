using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface INetworkGuestRepository
    {
        EntityFrameworkNetworkModel[] Get(User user);
        User[] Get(EntityFrameworkNetworkModel network);
        void Add(EntityFrameworkNetworkModel network, User user);
        void Remove(EntityFrameworkNetworkModel network, User user);
        bool Check(EntityFrameworkNetworkModel network, User user);
    }
}
