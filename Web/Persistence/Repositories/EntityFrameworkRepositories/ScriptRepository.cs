using System;
using System.Data.Entity;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class ScriptRepository : IScriptRepository
    {
        private readonly DbSet<EntityFrameworkScriptModel> _scripts;
        private readonly Action _save;

        public ScriptRepository(DbSet<EntityFrameworkScriptModel> scripts, Action save)
        {
            _scripts = scripts;
            _save = save;
        }

        public Script Get(int id)
        {
            var result = _scripts.Find(id);

            if (result == null)
            {
                return null;
            }

            return result.ToRepositoryType();
        }

        public Page<Script> List(ListFilter filter)
        {
            var results = _scripts
                .Page(filter, x => x.Id)
                .Transform(x => x.ToRepositoryType())
                ;

            return results;
        }

        public void Add(Script script)
        {
            var model = EntityFrameworkScriptModel.FromRepositoryType(script);

            _scripts.Add(model);

            _save();

            script.SetId(model.Id);
        }

        public void Remove(Script script)
        {
            var model = _scripts.Find(script.Id);

            _scripts.Remove(model);
        }
    }
}
