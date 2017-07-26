using System;
using System.Linq;
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
            var cache = new InMemoryRepositoryModelCache();
            var task = _taskRepository.Get(User, id, cache);

            if (task == null)
            {
                throw new HttpException(404, "Task not found");
            }

            var result = GetSerializableVersion(task);

            return result;
        }

        public Page<Persistence.Models.Task> Get([FromUri] ListFilter filter)
        {
            var cache = new InMemoryRepositoryModelCache();
            var result = _taskRepository.List(User, filter, cache)
                .Transform(GetSerializableVersion);

            return result;
        }

        public object Post(string action, string computerName = null, TimeSpan? pollInterval = null, TimeSpan ? timeout = null)
        {
            object result;

            switch(action)
            {
                case "Clean":
                    var clean = new Actions.Clean(_taskRepository);
                    result = clean.Run(timeout, User);
                    break;

                case "GetForComputer":
                    var cache = new InMemoryRepositoryModelCache();
                    var computerRepository = RepositoryFactory.GetComputerRepository();
                    var taskRepository = RepositoryFactory.GetTaskRepository();
                    var computer = Computer ?? computerRepository.Get(User, computerName, cache);
                    var getTasks = new Actions.GetForComputer(computerRepository, taskRepository);
                    var tasks = getTasks.Run(computer, timeout, pollInterval);
                    result = tasks
                        .Select(GetSerializableVersion)
                        .ToArray();
                    break;

                default:
                    throw new NotImplementedException();
            }

            return result;
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
