﻿using System.Web.Http;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api
{
    [ApiRestrictedAccessAttribute]
    [AutoSave]
    public class TaskController : RoomieBaseApiController
    {
        public TaskModel Get(int id)
        {
            var task = Database.Tasks.Get(id);
            var result = GetSerializableVersion(task);

            return result;
        }

        public Page<TaskModel> Get([FromUri] ListFilter filter)
        {
            var result = Database.Tasks.List(User, filter)
                .Transform(GetSerializableVersion);

            return result;
        }

        private TaskModel GetSerializableVersion(TaskModel task)
        {
            var result = new TaskModel
            {
                Expiration = task.Expiration,
                Id = task.Id,
                Origin = task.Origin,
                ReceivedTimestamp = task.ReceivedTimestamp,
                Script = task.Script
            };

            if (task.Owner != null)
            {
                result.Owner = new UserModel
                {
                    Id = task.Owner.Id
                };
            }

            if (task.Target != null)
            {
                result.Target = new ComputerModel
                {
                    Id = task.Target.Id
                };
            }

            return task;
        }
    }
}
