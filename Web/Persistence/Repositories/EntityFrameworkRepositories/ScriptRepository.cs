using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class ScriptRepository : IScriptRepository
    {
        private readonly DbSet<ScriptModel> _scripts;

        public ScriptRepository(DbSet<ScriptModel> scripts)
        {
            _scripts = scripts;
        }

        public ScriptModel Get(int id)
        {
            var result = _scripts.Find(id);

            return result;
        }

        public ScriptModel[] List(ListFilter filter)
        {
            var results = _scripts
                .Page(filter, x => x.Id)
                .ToArray();

            return results;
        }

        public void Add(ScriptModel script)
        {
            _scripts.Add(script);
        }

        public void Remove(ScriptModel script)
        {
            _scripts.Remove(script);
        }
    }
}
