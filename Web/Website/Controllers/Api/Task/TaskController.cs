using System;
using System.Web;
using System.Web.Http;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Task
{
    [ApiRestrictedAccess]
    public class TaskController : BaseController
    {
        private ITaskRepository _taskRepository;

        public TaskController()
        {
            _taskRepository = RepositoryFactory.GetTaskRepository();
        }

        public Persistence.Models.Task Get(int id)
        {
            var task = _taskRepository.Get(User, id);

            if (task == null)
            {
                throw new HttpException(404, "Task not found");
            }

            var result = GetSerializableVersion(task);

            return result;
        }

        public Page<Persistence.Models.Task> Get([FromUri] ListFilter filter)
        {
            var result = _taskRepository.List(User, filter)
                .Transform(GetSerializableVersion);

            return result;
        }

        public object Post(string action, int? timeout = null)
        {
            switch(action)
            {
                case "Clean":
                    return Clean(timeout);

                default:
                    throw new NotImplementedException();
            }
        }

        private string Clean(int? timeout)
        {
            if (timeout < 1)
            {
                timeout = null;
            }

            var deleted = 0;
            var skipped = 0;
            ListFilter filter = null;

            DoWork.UntilTimeout(timeout ?? 5, () =>
            {
                var result = _taskRepository.Clean(User, filter);

                deleted += result.Deleted;
                skipped += result.Skipped;
                filter = result.NextFilter;

                return result.Done;
            });

            return deleted + " tasks cleaned up, " + skipped + " tasks skipped";
        }

        private Persistence.Models.Task GetSerializableVersion(Persistence.Models.Task task)
        {
            Persistence.Models.User owner = null;
            if (task.Owner != null)
            {
                owner = new Persistence.Models.User(
                    alias: task.Owner.Alias,
                    email: null,
                    id: task.Owner.Id,
                    registeredTimestamp: null,
                    secret: null,
                    token: null
                );
            }

            Persistence.Models.Computer target = null;
            if (task.Target != null)
            {
                target = new Persistence.Models.Computer(
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

            var result = new Persistence.Models.Task(
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
