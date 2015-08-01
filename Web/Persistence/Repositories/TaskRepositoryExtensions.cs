using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public static class TaskRepositoryExtensions
    {
        public static CleaningResult Clean(this ITaskRepository repository, EntityFrameworkUserModel user, ListFilter filter = null)
        {
            if (filter == null)
            {
                filter = new ListFilter
                    {
                        SortDirection = SortDirection.Ascending
                    };
            }

            var allTasks = repository.List(user, filter).Items;

            var oldTasks = allTasks.Where(x => x.Received || x.Expired).ToArray();

            foreach (var task in allTasks)
            {
                repository.Remove(task);
            }

            var result = new CleaningResult(filter, allTasks.Length, oldTasks.Length);

            return result;
        }
    }
}
