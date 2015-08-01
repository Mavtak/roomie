using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IScriptRepository
    {
        EntityFrameworkScriptModel Get(int id);
        Page<EntityFrameworkScriptModel> List(ListFilter filter);
        void Add(EntityFrameworkScriptModel script);
        void Remove(EntityFrameworkScriptModel script);
    }
}
