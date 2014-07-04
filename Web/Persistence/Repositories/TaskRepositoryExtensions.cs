using System;
using System.Diagnostics;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public static class TaskRepositoryExtensions
    {
        public static int Clean(this ITaskRepository repository, UserModel user, Action save, int timeout)
        {
            var count = 0;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.Elapsed.TotalSeconds < timeout)
            {
                var tasks = repository.List(user, new ListFilter
                {
                    SortDirection = SortDirection.Ascending
                });

                tasks = tasks.Where(x => x.Received || x.Expired).ToArray();

                if (!tasks.Any())
                {
                    return count;
                }

                foreach (var task in tasks)
                {
                    repository.Remove(task);

                    count++;
                }

                if (save != null)
                {
                    save();
                }
            }

            return count;
        }
    }
}
