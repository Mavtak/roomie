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

        public void Post(Computer model)
        {
            var computer = Computer.Create(model.Name, User);
            Database.Computers.Add(computer);
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