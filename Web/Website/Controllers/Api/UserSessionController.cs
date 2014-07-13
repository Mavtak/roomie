using System.Web.Http;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api
{
    [ApiRestrictedAccessAttribute]
    [AutoSave]
    public class UserSessionController : RoomieBaseApiController
    {
        public UserSessionModel Get(string token)
        {
            var result = Database.Sessions.GetUserSession(token);

            return result;
        }

        public Page<object> Get([FromUri] ListFilter filter)
        {
            var sessions = Database.Sessions.ListUserSessions(User, filter);

            var result = sessions.Transform(Transform);

            return result;
        }

        private static object Transform(UserSessionModel model)
        {
            var result = new
                {
                    Id = model.Id,
                    CreationTimeStamp = model.CreationTimeStamp,
                    User = model.User.Id,
                    LastContactTimeStamp = model.LastContactTimeStamp,
                };

            return result;
        }
    }
}
