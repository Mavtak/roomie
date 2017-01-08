using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.Helpers
{
    public class RoomieBaseApiController : ApiController, IRoomieController
    {
        public static string SessionTokenName = "roomie_session";

        public IRoomieDatabaseContext Database { get; set; }
        public new User User { get; set; }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Database = new RoomieDatabaseContext();

            var userSession = GetCurrentUserSession();

            if (userSession != null)
            {
                User = userSession.User;
            }

            base.Initialize(controllerContext);
        }

        private UserSession GetCurrentUserSession()
        {
            var request = HttpContext.Current.Request;
            if (request.Cookies[SessionTokenName] == null)
            {
                return null;
            }

            var token = request.Cookies[SessionTokenName].Value;

            var session = Database.Sessions.GetUserSession(token);

            if (session == null)
            {
                return null;
            }

            session.UpdateLastContact();
            Database.Sessions.Update(session);

            return session;
        }
    }
}