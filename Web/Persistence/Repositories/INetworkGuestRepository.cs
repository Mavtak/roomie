using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface INetworkGuestRepository
    {
        EntityFrameworkNetworkModel[] Get(EntityFrameworkUserModel user);
        EntityFrameworkUserModel[] Get(EntityFrameworkNetworkModel network);
        void Add(EntityFrameworkNetworkModel network, EntityFrameworkUserModel user);
        void Remove(EntityFrameworkNetworkModel network, EntityFrameworkUserModel user);
        bool Check(EntityFrameworkNetworkModel network, EntityFrameworkUserModel user);
    }
}
