using System.Web.Http;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api
{
    [ApiRestrictedAccessAttribute]
    [AutoSave]
    public class TaskController : RoomieBaseApiController
    {
        public ShallowTaskModel Get(int id)
        {
            var task = Database.Tasks.Get(id);
            var result = ShallowTaskModel.FromTaskModel(task);

            return result;
        }

        public Page<ShallowTaskModel> Get([FromUri] ListFilter filter)
        {
            var result = Database.Tasks.List(User, filter)
                .Transform(ShallowTaskModel.FromTaskModel);

            return result;
        }
    }
}
