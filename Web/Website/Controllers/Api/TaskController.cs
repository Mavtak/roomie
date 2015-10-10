﻿using System.Web.Http;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api
{
    [ApiRestrictedAccess]
    public class TaskController : RoomieBaseApiController
    {
        public Task Get(int id)
        {
            var task = Database.Tasks.Get(id);
            var result = GetSerializableVersion(task);

            return result;
        }

        public Page<Task> Get([FromUri] ListFilter filter)
        {
            var result = Database.Tasks.List(User, filter)
                .Transform(GetSerializableVersion);

            return result;
        }

        private Task GetSerializableVersion(Task task)
        {
            User owner = null;
            if (task.Owner != null)
            {
                owner = new User(
                    alias: task.Owner.Alias,
                    email: null,
                    id: task.Owner.Id,
                    registeredTimestamp: null,
                    secret: null,
                    token: null
                );
            }

            Computer target = null;
            if (task.Target != null)
            {
                target = new Computer(
                    accessKey: null,
                    address: null,
                    encryptionKey: null,
                    id: task.Target.Id,
                    lastPing: null,
                    lastScript: null,
                    name: task.Target.Name,
                    owner: null
                );
            }

            var result = new Task(
                expiration: task.Expiration,
                id: task.Id,
                origin: task.Origin,
                owner: owner,
                receivedTimestamp: task.ReceivedTimestamp,
                script: task.Script,
                target: target
            );

            return result;
        }
    }
}
