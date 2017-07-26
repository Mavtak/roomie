using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IUserRepository
    {
        User Get(int id, IRepositoryModelCache cache = null);
        User Get(string token, IRepositoryModelCache cache = null);
        void Add(User user);
        void Update(User user);
    }
}
