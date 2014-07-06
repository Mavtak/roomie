using System;
using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class SavedScriptRepository : ISavedScriptRepository
    {
        private readonly DbSet<SavedScriptModel> _scripts;

        public SavedScriptRepository(DbSet<SavedScriptModel> scripts)
        {
            _scripts = scripts;
        }

        public SavedScriptModel Get(int id)
        {
            var result = _scripts.Find(id);

            return result;
        }

        public SavedScriptModel Get(UserModel user, int id)
        {
            var result = Get(id);

            if (result == null)
            {
                return null;
            }

            if (result.Owner == null)
            {
                throw new Exception("Owner not set");
            }

            if (result.Owner.Id != user.Id)
            {
                result = null;
            }

            return result;
        }

        public SavedScriptModel[] Get(ScriptModel script)
        {
            var result = _scripts
                .Where(x => x.Script.Id == script.Id)
                .ToArray();

            return result;
        }

        public SavedScriptModel[] List(UserModel user, int page, int count)
        {
            var results = (from t in _scripts
                           where t.Owner.Id == user.Id
                           orderby t.Script.CreationTimestamp descending
                           select t).Skip((page - 1) * count).Take(count)
                         .ToArray();

            return results;
        }

        public void Add(SavedScriptModel script)
        {
            _scripts.Add(script);
        }

        public void Remove(SavedScriptModel script)
        {
            _scripts.Remove(script);
        }
    }
}
