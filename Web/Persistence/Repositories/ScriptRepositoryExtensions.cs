using System;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public static class ScriptRepositoryExtensions
    {
        public static int Clean(this IScriptRepository repository, ITaskRepository taskRepository, ISavedScriptRepository savedScriptRepository, IComputerRepository computerRepository)
        {
            var scripts = repository.List(new ListFilter
            {
                SortDirection = SortDirection.Ascending
            }).Items;

            scripts = scripts.Where(x =>
                {
                    var getOwnersFunctions = new Func<ScriptModel, object[]>[]
                        {
                            taskRepository.Get,
                            savedScriptRepository.Get,
                            computerRepository.Get
                        };

                    foreach (var getOwnersFunction in getOwnersFunctions)
                    {
                        var owners = getOwnersFunction(x);

                        if (owners.Any())
                        {
                            return false;
                        }
                    }

                    return true;
                }).ToArray();

            if (!scripts.Any())
            {
                return 0;
            }

            var count = 0;

            foreach (var task in scripts)
            {
                repository.Remove(task);

                count++;
            }

            return count;
        }
    }
}
