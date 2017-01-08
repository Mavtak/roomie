using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.Helpers
{
    public class RoomieBaseApiController : ApiController
    {
        public static string SessionTokenCookieName = "roomie_session";
        public static string WebHookSessionTokenHeaderName = "x-roomie-webhook-session";

        public IRoomieDatabaseContext Database { get; set; }
        public Computer Computer { get; private set; }
        public new User User { get; set; }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Database = new RoomieDatabaseContext();

            var userSession = GetCurrentUserSession(controllerContext.Request);

            if (userSession != null)
            {
                User = userSession.User;
            }

            var webHookSession = GetCurrentWebHookSession(controllerContext.Request);

            if (webHookSession != null)
            {
                Computer = webHookSession.Computer;
            }

            base.Initialize(controllerContext);
        }

        protected void AddTask(Computer computer, string origin, string scriptText)
        {
            var script = Script.Create(false, scriptText);
            Database.Scripts.Add(script);

            var task = Task.Create(User, origin, computer, script);

            Database.Tasks.Add(task);
        }

        private UserSession GetCurrentUserSession(HttpRequestMessage request)
        {
            var cookie = request.Headers.GetCookies(SessionTokenCookieName)
                .FirstOrDefault();

            if (cookie == null)
            {
                return null;
            }

            var token = cookie[SessionTokenCookieName].Value;

            var session = Database.Sessions.GetUserSession(token);

            if (session == null)
            {
                return null;
            }

            session.UpdateLastContact();
            Database.Sessions.Update(session);

            return session;
        }

        private WebHookSession GetCurrentWebHookSession(HttpRequestMessage request)
        {
            if (!request.Headers.Contains(WebHookSessionTokenHeaderName))
            {
                return null;
            }

            var token = request.Headers.GetValues(WebHookSessionTokenHeaderName)
                .FirstOrDefault();

            if (token == null)
            {
                return null;
            }

            var session = Database.Sessions.GetWebHookSession(token);

            if (session == null)
            {
                return null;
            }

            session.Computer.UpdatePing();
            Database.Computers.Update(session.Computer);

            return session;
        }
    }
}