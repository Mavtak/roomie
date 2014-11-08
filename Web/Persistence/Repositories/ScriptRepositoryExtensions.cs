using System;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public static class ScriptRepositoryExtensions
    {
        public static CleaningResult Clean(this IScriptRepository repository, ITaskRepository taskRepository, ISavedScriptRepository savedScriptRepository, IComputerRepository computerRepository, ListFilter filter = null)
        {
            if (filter == null)
            {
                filter = new ListFilter
                {
                    SortDirection = SortDirection.Ascending
                };
            }

            var allScripts = repository.List(filter).Items;

            var unusedScripts = allScripts.Where(x =>
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


            foreach (var script in unusedScripts)
            {
                repository.Remove(script);
            }

            var result = new CleaningResult(filter, allScripts.Length, unusedScripts.Length);

            return result;
        }
    }
}
