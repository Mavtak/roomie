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
        public EntityFrameworkTaskModel Get(int id)
        {
            var task = Database.Tasks.Get(id);
            var result = GetSerializableVersion(task);

            return result;
        }

        public Page<EntityFrameworkTaskModel> Get([FromUri] ListFilter filter)
        {
            var result = Database.Tasks.List(User, filter)
                .Transform(GetSerializableVersion);

            return result;
        }

        private EntityFrameworkTaskModel GetSerializableVersion(EntityFrameworkTaskModel task)
        {
            var result = new EntityFrameworkTaskModel
            {
                Expiration = task.Expiration,
                Id = task.Id,
                Origin = task.Origin,
                ReceivedTimestamp = task.ReceivedTimestamp,
                Script = task.Script
            };

            if (task.Owner != null)
            {
                result.Owner = new EntityFrameworkUserModel
                {
                    Id = task.Owner.Id
                };
            }

            if (task.Target != null)
            {
                result.Target = new EntityFrameworkComputerModel
                {
                    Id = task.Target.Id,
                    Name = task.Target.Name
                };
            }

            return result;
        }
    }
}
