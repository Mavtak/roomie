using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IUserRepository
    {
        EntityFrameworkUserModel Get(int id);
        EntityFrameworkUserModel Get(string token);
        void Add(EntityFrameworkUserModel user);
    }
}
