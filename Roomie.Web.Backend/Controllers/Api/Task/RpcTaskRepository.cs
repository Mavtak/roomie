using System;
using System.Linq;
using Roomie.Common;
using Roomie.Common.Api.Models;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Backend.Controllers.Api.Task
{
    public class RpcTaskRepository
    {
        private Persistence.Models.Computer _computer;
        private IRepositoryFactory _repositoryFactory;
        private ITaskRepository _taskRepository;
        private Persistence.Models.User _user;

        public RpcTaskRepository(Persistence.Models.Computer computer, IRepositoryFactory repositoryFactory, Persistence.Models.User user)
        {
            _computer = computer;
            _repositoryFactory = repositoryFactory;
            _user = user;

            _taskRepository = _repositoryFactory.GetTaskRepository();
        }

        public Response<Persistence.Models.Task> Read(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var task = _taskRepository.Get(_user, id, cache);
            var result = GetSerializableVersion(task);

            return Response.Create(result);
        }

        public Response<Page<Persistence.Models.Task>> List(int count = 50, string sortDirection = null, int start = 0)
        {
            var filter = new ListFilter
            {
                Count = count,
                SortDirection = sortDirection == null ? SortDirection.Descending : EnumParser.Parse<SortDirection>(sortDirection),
                Start = start,
            };

            var cache = new InMemoryRepositoryModelCache();
            var result = _taskRepository.List(_user, filter, cache)
                .Transform(GetSerializableVersion);

            return Response.Create(result);
        }

        public Response<string> Clean(TimeSpan? timeout = null)
        {
            var clean = new Actions.Clean(_taskRepository);
            var result = clean.Run(timeout, _user);

            return Response.Create(result);
        }

        public Response<Persistence.Models.Task[]> GetForComputer(string computerName, TimeSpan? timeout = null, TimeSpan? pollInterval = null)
        {
            var cache = new InMemoryRepositoryModelCache();
            var computerRepository = _repositoryFactory.GetComputerRepository();
            var computer = _computer ?? computerRepository.Get(_user, computerName, cache);

            var taskRepository = _repositoryFactory.GetTaskRepository();
            var getTasks = new Actions.GetForComputer(computerRepository, taskRepository);
            var tasks = getTasks.Run(computer, timeout, pollInterval);
            var result = tasks
                .Select(GetSerializableVersion)
                .ToArray();

            return Response.Create(result);
        }

        private Persistence.Models.Task GetSerializableVersion(Persistence.Models.Task task)
        {
            if (task == null)
            {
                return null;
            }

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