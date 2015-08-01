using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface ISavedScriptRepository
    {
        EntityFrameworkSavedScriptModel Get(int id);
        EntityFrameworkSavedScriptModel Get(User user, int id);
        EntityFrameworkSavedScriptModel[] Get(EntityFrameworkScriptModel script);
        EntityFrameworkSavedScriptModel[] List(User user, int page, int count);
        void Add(EntityFrameworkSavedScriptModel script);
        void Remove(EntityFrameworkSavedScriptModel script);
    }
}
