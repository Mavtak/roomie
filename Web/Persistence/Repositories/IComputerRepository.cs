
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IComputerRepository
    {
        ComputerModel Get(int id);
        ComputerModel Get(UserModel user, int id);
        ComputerModel Get(string accessKey);
        ComputerModel Get(UserModel user, string name);
        ComputerModel[] Get(UserModel user);
        void Add(ComputerModel computer);
        void Remove(ComputerModel computer);
    }
}
