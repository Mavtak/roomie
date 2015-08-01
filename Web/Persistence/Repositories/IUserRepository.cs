using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IUserRepository
    {
        User Get(int id);
        User Get(string token);
        void Add(User user);
    }
}
