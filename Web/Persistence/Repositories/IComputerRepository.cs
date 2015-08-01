
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IComputerRepository
    {
        EntityFrameworkComputerModel Get(int id);
        EntityFrameworkComputerModel Get(User user, int id);
        EntityFrameworkComputerModel Get(string accessKey);
        EntityFrameworkComputerModel Get(User user, string name);
        EntityFrameworkComputerModel[] Get(EntityFrameworkScriptModel script);
        EntityFrameworkComputerModel[] Get(User user);
        void Add(EntityFrameworkComputerModel computer);
        void Remove(EntityFrameworkComputerModel computer);
    }
}
