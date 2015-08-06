
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IComputerRepository
    {
        Computer Get(int id);
        Computer Get(User user, int id);
        Computer Get(string accessKey);
        Computer Get(User user, string name);
        Computer[] Get(EntityFrameworkScriptModel script);
        Computer[] Get(User user);
        void Add(Computer computer);
        void Update(Computer computer);
        void Remove(Computer computer);
    }
}
