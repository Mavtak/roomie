using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface INetworkRepository
    {
        Network Get(int id);
        Network Get(User user, int id);
        Network[] Get(User user);
        Network Get(User user, string address);
        void Add(Network network);
        void Update(Network network);
        void Remove(Network network);
    }
}
