using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IScriptRepository
    {
        Script Get(int id);
        Page<Script> List(ListFilter filter);
        void Add(Script script);
        void Remove(Script script);
    }
}
