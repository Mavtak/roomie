using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IUserRepository
    {
        UserModel Get(int id);
        UserModel Get(string token);
        void Add(UserModel user);
    }
}
