using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface INetworkGuestRepository
    {
        Network[] Get(User user);
        User[] Get(Network network);
        void Add(Network network, User user);
        void Remove(Network network, User user);
        bool Check(Network network, User user);
    }
}
