using System;
using System.Linq;
using System.Web;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Computer
{
    [ApiRestrictedAccess]
    public class ComputerController : BaseController
    {
        private IComputerRepository _computerRepository;
        private IScriptRepository _scriptRepository;
        private ITaskRepository _taskRepository;

        public ComputerController()
        {
            _computerRepository = RepositoryFactory.GetComputerRepository();
            _scriptRepository = RepositoryFactory.GetScriptRepository();
            _taskRepository = RepositoryFactory.GetTaskRepository();
        }

        public Persistence.Models.Computer[] Get()
        {
            var computers = _computerRepository.Get(User);
            var result = computers.Select(GetSerializableVersion)
                .ToArray();

            return result;
        }

        public Persistence.Models.Computer Get(int id)
        {
            var computer = SelectComputer(id);
            var result = GetSerializableVersion(computer);

            return result;
        }

        public Persistence.Models.Computer Get(string accessKey)
        {
            var computer = _computerRepository.Get(accessKey);
            var result = GetSerializableVersion(computer);
            return result;
        }

        public void Post(AddComputerModel add)
        {
            add = add ?? new AddComputerModel();

            var computer = Persistence.Models.Computer.Create(add.Name, User);
            _computerRepository.Add(computer);
        }

        public void Post(int id, string action, ComputerActionOptions options)
        {
            options = options ?? new ComputerActionOptions();

            var computer = SelectComputer(id);

            switch(action)
            {
                case "DisableWebHook":
                    computer.DisableWebhook();
                    _computerRepository.Update(computer);
                    break;

                case "RenewWebHookKeys":
                    computer.RenewWebhookKeys();
                    _computerRepository.Update(computer);
                    break;

                case "RunScript":
                    var runScript = new Actions.RunScript(_computerRepository, _scriptRepository, _taskRepository);
                    runScript.Run(
                        computer: computer,
                        scriptText: options.Script,
                        source: "Website",
                        updateLastRunScript: true,
                        user: User
                    );
                    
                    break;

                case "SendScript":

                    break;

                default:
                    throw new NotImplementedException();
            }
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

        private Persistence.Models.Computer SelectComputer(int id)
        {
            var computer = _computerRepository.Get(User, id);

            if (computer == null)
            {
                throw new HttpException(404, "Computer not found");
            }

            return computer;
        }
    }
}