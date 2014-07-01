using System.Linq;
using Roomie.Web.Persistence.Models;
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

        public object[] Get(int? page, int? count)
        {
            var sessions = Database.Sessions.ListUserSessions(User, page, count);
            var result = sessions.Select(Transform).ToArray();

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
