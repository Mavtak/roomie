using System;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories.Models
{
    public class ScriptModel
    {
        public DateTime? CreationTimestamp { get; set; }
        public int Id { get; set; }
        public DateTime? LastRunTimestamp { get; set; }
        public bool? Mutable { get; set; }
        public int? RunCount { get; set; }
        public string Text { get; set; }

        public static ScriptModel FromRepositoryType(Script model)
        {
            if (model == null)
            {
                return null;
            }

            var result = new ScriptModel
            {
                CreationTimestamp = model.CreationTimestamp,
                Id = model.Id,
                LastRunTimestamp = model.LastRunTimestamp,
                Mutable = model.Mutable,
                RunCount = model.RunCount,
                Text = model.Text
            };

            return result;
        }

        public Script ToRepositoryType()
        {
            var result = new Script(
                creationTimestamp: CreationTimestamp,
                id: Id,
                lastRunTimestamp: LastRunTimestamp,
                mutable: Mutable,
                runCount: RunCount,
                text: Text
            );

            return result;
        }

        public static Script ToRepositoryType(int? id, IScriptRepository scriptRepository)
        {
            if (id == null)
            {
                return null;
            }

            if (scriptRepository == null)
            {
                return new ScriptModel
                {
                    Id = id.Value
                }.ToRepositoryType();
            }

            return scriptRepository.Get(id.Value);
        }
    }
}
