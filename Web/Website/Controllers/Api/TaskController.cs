using System.Web.Http;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api
{
    [ApiRestrictedAccess]
    [AutoSave]
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

            EntityFrameworkComputerModel target = null;
            if (task.Target != null)
            {
                target = new EntityFrameworkComputerModel
                {
                    Id = task.Target.Id
                };
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
