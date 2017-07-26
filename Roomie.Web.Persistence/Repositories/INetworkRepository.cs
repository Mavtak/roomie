using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface INetworkRepository
    {
        Network Get(int id, IRepositoryModelCache cache = null);
        Network Get(User user, int id, IRepositoryModelCache cache = null);
        Network[] Get(User user, IRepositoryModelCache cache = null);
        Network Get(User user, string address, IRepositoryModelCache cache = null);
        void Add(Network network);
        void Update(Network network);
        void Remove(Network network);
    }
}
