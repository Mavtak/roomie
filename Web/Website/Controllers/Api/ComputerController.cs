using System.Linq;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api
{
    [ApiRestrictedAccess]
    [AutoSave]
    public class ComputerController : RoomieBaseApiController
    {
        public EntityFrameworkComputerModel[] Get()
        {
            var computers = Database.Computers.Get(User);
            var result = computers.Select(GetSerializableVersion)
                .ToArray();

            return result;
        }

        public EntityFrameworkComputerModel Get(int id)
        {
            var computer = this.SelectComputer(id);
            var result = GetSerializableVersion(computer);

            return result;
        }

        public EntityFrameworkComputerModel Get(string accessKey)
        {
            var computer = Database.Computers.Get(accessKey);
            var result = GetSerializableVersion(computer);
            return result;
        }

        public void Post(EntityFrameworkComputerModel model)
        {
            Database.Computers.Add(new EntityFrameworkComputerModel
            {
                Owner = User,
                Name = model.Name
            });
        }

        private static EntityFrameworkComputerModel GetSerializableVersion(EntityFrameworkComputerModel device)
        {
            var result = new EntityFrameworkComputerModel
            {
                AccessKey = device.AccessKey,
                Address = device.Address,
                EncryptionKey = device.EncryptionKey,
                Id = device.Id,
                LastPing = device.LastPing,
                LastScript = device.LastScript,
                Name = device.Name
            };

            if (device.Owner != null)
            {
                result.Owner = new EntityFrameworkUserModel
                {
                    Id = device.Owner.Id
                };
            }

            return result;
        }
    }
}