using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roomie.Web.Website.Helpers;
using Roomie.Web.Models;
using System.Diagnostics;

namespace Roomie.Web.Website.Controllers.Api
{
    [ApiRestrictedAccess]
    [AutoSave]
    public class TaskController : RoomieBaseApiController
    {

        public IQueryable<TaskModel> Get()
        {
            var tasks = from task in Database.Tasks
                        where task.Owner.Id == User.Id
                        select task;

            return tasks;
        }

        public TaskModel Get(int id)
        {
            var task = this.SelectTask(id);

            return task;
        }

        public void Post(int id, string action)
        {
            var task = this.SelectTask(id);

            throw new NotImplementedException();
        }

        public void Put(TaskModel task)
        {
            task.Owner = User;
            Database.Tasks.Add(task);
        }

        public void Delete(int id)
        {
            var task = this.SelectTask(id);
            Database.Tasks.Remove(task);
        }

    }
}
