using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Task.Actions
{
    public class Clean
    {
        private ITaskRepository _taskRepository;

        public Clean(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public string Run(TimeSpan? timeout, Persistence.Models.User user)
        {
            var deleted = 0;
            var skipped = 0;
            ListFilter filter = null;

            DoWork.UntilTimeout(timeout?.Seconds ?? 5, () =>
            {
                var result = _taskRepository.Clean(user, filter);

                deleted += result.Deleted;
                skipped += result.Skipped;
                filter = result.NextFilter;

                return result.Done;
            });

            return deleted + " tasks cleaned up, " + skipped + " tasks skipped";
        }
    }
}