﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Backend.Helpers;

namespace Roomie.Web.Backend.Controllers.Api.Task.Actions
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
            var cache = new InMemoryRepositoryModelCache();
            var deleted = 0;
            var skipped = 0;
            ListFilter filter = null;

            DoWork.UntilTimeout(((int?)timeout?.TotalSeconds) ?? 5, () =>
            {
                var result = _taskRepository.Clean(user, filter, cache);

                deleted += result.Deleted;
                skipped += result.Skipped;
                filter = result.NextFilter;

                return result.Done;
            });

            return deleted + " tasks cleaned up, " + skipped + " tasks skipped";
        }
    }
}