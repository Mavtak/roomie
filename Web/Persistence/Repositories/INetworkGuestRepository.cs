using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface INetworkGuestRepository
    {
        NetworkModel[] Get(UserModel user);
        UserModel[] Get(NetworkModel network);
        void Add(NetworkModel network, UserModel user);
        void Remove(NetworkModel network, UserModel user);
        bool Check(NetworkModel network, UserModel user);
    }
}
