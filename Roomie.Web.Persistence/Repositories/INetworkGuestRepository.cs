using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface INetworkGuestRepository
    {
        Network[] Get(User user, IRepositoryModelCache cache = null);
        User[] Get(Network network, IRepositoryModelCache cache = null);
        void Add(Network network, User user);
        void Remove(Network network, User user);
        bool Check(Network network, User user);
    }
}
