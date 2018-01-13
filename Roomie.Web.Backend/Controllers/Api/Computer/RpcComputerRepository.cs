using System.Linq;
using Roomie.Common.Api.Models;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Backend.Controllers.Api.Computer
{
    public class RpcComputerRepository
    {
        private IComputerRepository _computerRepository;
        private IRepositoryFactory _repositoryFactory;
        private Persistence.Models.User _user;

        public RpcComputerRepository(IRepositoryFactory repositoryFactory, Persistence.Models.User user)
        {
            _repositoryFactory = repositoryFactory;
            _user = user;

            _computerRepository = _repositoryFactory.GetComputerRepository();
        }

        public Response<Persistence.Models.Computer[]> List()
        {
            var cache = new InMemoryRepositoryModelCache();
            var computers = _computerRepository.Get(_user, cache);
            var result = computers.Select(GetSerializableVersion)
                .ToArray();

            return Response.Create(result);
        }

        public Response<Persistence.Models.Computer> Read(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var computer = _computerRepository.Get(_user, id, cache);
            var result = GetSerializableVersion(computer);

            return Response.Create(result);
        }

        public Response<Persistence.Models.Computer> Read(string accessKey)
        {
            var cache = new InMemoryRepositoryModelCache();
            var computer = _computerRepository.Get(accessKey, cache);
            var result = GetSerializableVersion(computer);

            return Response.Create(result);
        }

        public void Create(string name)
        {
            var computer = Persistence.Models.Computer.Create(name, _user);

            _computerRepository.Add(computer);
        }

        public Response DisableWebHook(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var computer = _computerRepository.Get(_user, id, cache);

            if (computer == null)
            {
                return RpcComputerRepositoryHelpers.CreateNotFoundError();
            }

            computer.DisableWebhook();
            _computerRepository.Update(computer);

            return Response.Empty();
        }

        public Response RenewWebHookKeys(int id)
        {
            var cache = new InMemoryRepositoryModelCache();
            var computer = _computerRepository.Get(_user, id, cache);

            if (computer == null)
            {
                return RpcComputerRepositoryHelpers.CreateNotFoundError();
            }

            computer.RenewWebhookKeys();
            _computerRepository.Update(computer);

            return Response.Empty();
        }

        public Response RunScript(int id, string script, string source = null)
        {
            var cache = new InMemoryRepositoryModelCache();
            var computer = _computerRepository.Get(_user, id, cache);

            if (computer == null)
            {
                return RpcComputerRepositoryHelpers.CreateNotFoundError();
            }

            return RunScript(computer, script, source);
        }

        public Response RunScript(string computerName, string script, string source = null)
        {
            var cache = new InMemoryRepositoryModelCache();
            var computer = _computerRepository.Get(_user, computerName, cache);

            if (computer == null)
            {
                return RpcComputerRepositoryHelpers.CreateNotFoundError();
            }

            return RunScript(computer, script, source);
        }

        private Response RunScript(Persistence.Models.Computer computer, string script, string source)
        {
            var scriptRepository = _repositoryFactory.GetScriptRepository();
            var taskRepository = _repositoryFactory.GetTaskRepository();

            var runScript = new Actions.RunScript(_computerRepository, scriptRepository, taskRepository);

            runScript.Run(
                computer: computer,
                scriptText: script,
                source: source ?? "Website",
                updateLastRunScript: true,
                user: _user
            );

            return Response.Empty();
        }

        private static Persistence.Models.Computer GetSerializableVersion(Persistence.Models.Computer computer)
        {
            Persistence.Models.User owner = null;
            if (computer.Owner != null)
            {
                owner = new Persistence.Models.User(
                    alias: computer.Owner.Alias,
                    email: null,
                    id: computer.Owner.Id,
                    registeredTimestamp: null,
                    secret: null,
                    token: null
                );
            }

            var result = new Persistence.Models.Computer(
                accessKey: computer.AccessKey,
                address: computer.Address,
                encryptionKey: computer.EncryptionKey,
                id: computer.Id,
                lastPing: computer.LastPing,
                lastScript: computer.LastScript,
                name: computer.Name,
                owner: owner
            );

            return result;
        }
    }
}