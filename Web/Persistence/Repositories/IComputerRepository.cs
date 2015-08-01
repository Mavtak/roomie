
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IComputerRepository
    {
        EntityFrameworkComputerModel Get(int id);
        EntityFrameworkComputerModel Get(EntityFrameworkUserModel user, int id);
        EntityFrameworkComputerModel Get(string accessKey);
        EntityFrameworkComputerModel Get(EntityFrameworkUserModel user, string name);
        EntityFrameworkComputerModel[] Get(EntityFrameworkScriptModel script);
        EntityFrameworkComputerModel[] Get(EntityFrameworkUserModel user);
        void Add(EntityFrameworkComputerModel computer);
        void Remove(EntityFrameworkComputerModel computer);
    }
}
