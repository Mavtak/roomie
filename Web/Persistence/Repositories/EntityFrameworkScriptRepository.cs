using System.Data.Entity;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public class EntityFrameworkScriptRepository : IScriptRepository
    {
        private readonly DbSet<ScriptModel> _scripts;

        public EntityFrameworkScriptRepository(DbSet<ScriptModel> scripts)
        {
            _scripts = scripts;
        }

        public ScriptModel Get(int id)
        {
            var result = _scripts.Find(id);

            return result;
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
