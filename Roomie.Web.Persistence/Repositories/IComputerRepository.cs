
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IComputerRepository
    {
        Computer Get(int id, IRepositoryModelCache cache = null);
        Computer Get(User user, int id, IRepositoryModelCache cache = null);
        Computer Get(string accessKey, IRepositoryModelCache cache = null);
        Computer Get(User user, string name, IRepositoryModelCache cache = null);
        Computer[] Get(Script script, IRepositoryModelCache cache = null);
        Computer[] Get(User user, IRepositoryModelCache cache = null);
        void Add(Computer computer);
        void Update(Computer computer);
        void Remove(Computer computer);
    }
}
