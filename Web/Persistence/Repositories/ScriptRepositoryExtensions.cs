using System.Linq;

namespace Roomie.Web.Persistence.Repositories
{
    public static class ScriptRepositoryExtensions
    {
        public static int Clean(this IScriptRepository repository, ITaskRepository taskRepository, ISavedScriptRepository savedScriptRepository, IComputerRepository computerRepository)
        {
            var scripts = repository.List(new ListFilter
            {
                SortDirection = SortDirection.Ascending
            });

            scripts = scripts.Where(x =>
                {
                    var taskOwners = taskRepository.Get(x);

                    if (taskOwners.Any())
                    {
                        return false;
                    }

                    var savedScriptOwners = savedScriptRepository.Get(x);

                    if (savedScriptOwners.Any())
                    {
                        return false;
                    }

                    var computerOwners = computerRepository.Get(x);

                    if (computerOwners.Any())
                    {
                        return false;
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
