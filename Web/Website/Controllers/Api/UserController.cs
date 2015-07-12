using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api
{
    [AutoSave]
    public class UserController : RoomieBaseApiController
    {
        public void Post(string username, string password)
        {
           Database.Users.Add(username, password);
        }
    }
}