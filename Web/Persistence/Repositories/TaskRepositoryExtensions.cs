using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public static class TaskRepositoryExtensions
    {
        public static int Clean(this ITaskRepository repository, UserModel user)
        {
            var tasks = repository.List(user, new ListFilter
            {
                SortDirection = SortDirection.Ascending
            }).Items;

            tasks = tasks.Where(x => x.Received || x.Expired).ToArray();

            if (!tasks.Any())
            {
                return 0;
            }

            var count = 0;

            foreach (var task in tasks)
            {
                repository.Remove(task);

                count++;
            }

            return count;
        }
    }
}
