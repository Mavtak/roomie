using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface ISavedScriptRepository
    {
        SavedScriptModel Get(int id);
        SavedScriptModel Get(UserModel user, int id);
        SavedScriptModel[] Get(ScriptModel script);
        SavedScriptModel[] List(UserModel user, int page, int count);
        void Add(SavedScriptModel script);
        void Remove(SavedScriptModel script);
    }
}
