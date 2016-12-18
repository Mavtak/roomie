using System;
using System.Linq;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api
{
    [ApiRestrictedAccess]
    public class ComputerController : RoomieBaseApiController
    {
        public Computer[] Get()
        {
            var computers = Database.Computers.Get(User);
            var result = computers.Select(GetSerializableVersion)
                .ToArray();

            return result;
        }

        public Computer Get(int id)
        {
            var computer = this.SelectComputer(id);
            var result = GetSerializableVersion(computer);

            return result;
        }

        public Computer Get(string accessKey)
        {
            var computer = Database.Computers.Get(accessKey);
            var result = GetSerializableVersion(computer);
            return result;
        }

        public void Post(AddComputerModel add)
        {
            add = add ?? new AddComputerModel();

            var computer = Computer.Create(add.Name, User);
            Database.Computers.Add(computer);
        }

        public void Post(int id, string action, ComputerActionOptions options)
        {
            options = options ?? new ComputerActionOptions();

            var computer = this.SelectComputer(id);

            switch(action)
            {
                case "DisableWebHook":
                    computer.DisableWebhook();
                    Database.Computers.Update(computer);
                    break;

                case "RenewWebHookKeys":
                    computer.RenewWebhookKeys();
                    Database.Computers.Update(computer);
                    break;

                case "RunScript":
                    var scriptObject = Script.Create(false, options.Script);
                    Database.Scripts.Add(scriptObject);

                    var task = Task.Create(User, "Website", computer, scriptObject);

                    Database.Tasks.Add(task);

                    computer.UpdateLastScript(task.Script);
                    Database.Computers.Update(computer);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        private static Computer GetSerializableVersion(Computer computer)
        {
            User owner = null;
            if (computer.Owner != null)
            {
                owner = new User(
                    alias: computer.Owner.Alias,
                    email: null,
                    id: computer.Owner.Id,
                    registeredTimestamp: null,
                    secret: null,
                    token: null
                );
            }

            var result = new Computer(
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