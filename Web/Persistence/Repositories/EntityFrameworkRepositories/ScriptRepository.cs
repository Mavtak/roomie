using System.Data.Entity;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class ScriptRepository : IScriptRepository
    {
        private readonly DbSet<EntityFrameworkScriptModel> _scripts;

        public ScriptRepository(DbSet<EntityFrameworkScriptModel> scripts)
        {
            _scripts = scripts;
        }

        public EntityFrameworkScriptModel Get(int id)
        {
            var result = _scripts.Find(id);

            return result;
        }

        public Page<EntityFrameworkScriptModel> List(ListFilter filter)
        {
            var results = _scripts
                .Page(filter, x => x.Id)
                ;

            return results;
        }

        public void Add(EntityFrameworkScriptModel script)
        {
            _scripts.Add(script);
        }

        public void Remove(EntityFrameworkScriptModel script)
        {
            _scripts.Remove(script);
        }
    }
}
