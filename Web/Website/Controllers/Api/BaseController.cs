using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Roomie.Web.Persistence.Database;

namespace Roomie.Web.Website.Controllers.Api
{
    public class BaseController : ApiController
    {
        public static string SessionTokenCookieName = "roomie_session";
        public static string WebHookSessionTokenHeaderName = "x-roomie-webhook-session";

        public IRoomieDatabaseContext Database { get; set; }
        public Persistence.Models.Computer Computer { get; private set; }
        public new Persistence.Models.User User { get; private set; }

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

        protected void AddTask(Persistence.Models.Computer computer, string origin, string scriptText)
        {
            var script = Persistence.Models.Script.Create(false, scriptText);
            Database.Scripts.Add(script);

            var task = Persistence.Models.Task.Create(User, origin, computer, script);

            Database.Tasks.Add(task);
        }

        private Persistence.Models.UserSession GetCurrentUserSession(HttpRequestMessage request)
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

        private Persistence.Models.WebHookSession GetCurrentWebHookSession(HttpRequestMessage request)
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