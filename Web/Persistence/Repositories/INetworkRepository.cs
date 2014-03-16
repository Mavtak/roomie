using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface INetworkRepository
    {
        NetworkModel Get(int id);
        NetworkModel[] Get(UserModel user);
        NetworkModel Get(UserModel user, string address);
        void Add(NetworkModel network);
        void Remove(NetworkModel network);
    }
}
