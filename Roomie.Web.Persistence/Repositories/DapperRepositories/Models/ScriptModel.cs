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

        public static ScriptModel FromRepositoryType(Script repositoryModel)
        {
            if (repositoryModel == null)
            {
                return null;
            }

            var result = new ScriptModel
            {
                CreationTimestamp = repositoryModel.CreationTimestamp,
                Id = repositoryModel.Id,
                LastRunTimestamp = repositoryModel.LastRunTimestamp,
                Mutable = repositoryModel.Mutable,
                RunCount = repositoryModel.RunCount,
                Text = repositoryModel.Text
            };

            return result;
        }

        public Script ToRepositoryType(IRepositoryModelCache cache)
        {
            var result = new Script(
                creationTimestamp: CreationTimestamp,
                id: Id,
                lastRunTimestamp: LastRunTimestamp,
                mutable: Mutable,
                runCount: RunCount,
                text: Text
            );

            cache?.Set(result.Id, result);

            return result;
        }

        public static Script ToRepositoryType(IRepositoryModelCache cache, int? id, IScriptRepository scriptRepository)
        {
            if (id == null)
            {
                return null;
            }

            var cachedValue = cache?.Get<Script>(id);

            if (cachedValue != null)
            {
                return cachedValue;
            }

            if (scriptRepository == null)
            {
                return new ScriptModel
                {
                    Id = id.Value
                }.ToRepositoryType((IRepositoryModelCache)null);
            }

            var result = scriptRepository.Get(id.Value);

            cache?.Set(result.Id, result);

            return result;
        }
    }
}
