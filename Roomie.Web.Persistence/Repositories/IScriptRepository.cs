using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IScriptRepository
    {
        Script Get(int id, IRepositoryModelCache cache = null);
        Page<Script> List(ListFilter filter, IRepositoryModelCache cache = null);
        void Add(Script script);
        void Remove(Script script);
    }
}
