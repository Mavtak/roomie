using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Persistence.Repositories.DapperRepositories;
using Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories;

namespace Roomie.Web.Website.Controllers.Api
{
    public class BaseController : ApiController
    {
        public static string SessionTokenCookieName = "roomie_session";
        public static string WebHookSessionTokenHeaderName = "x-roomie-webhook-session";
        public static string WebHookAccessKeyHeaderName = "x-roomie-webhook-key";

        private EntityFrameworkRoomieDatabaseBackend _entityFrameworkRoomieDatabaseBackend;
        protected IRepositoryFactory RepositoryFactory { get; private set; }

        public Persistence.Models.Computer Computer { get; private set; }
        public new Persistence.Models.User User { get; private set; }

        protected BaseController()
        {
            _entityFrameworkRoomieDatabaseBackend = new EntityFrameworkRoomieDatabaseBackend();
            RepositoryFactory = new CompositeImplementationRepositoryFactory(
                new DapperRepositoryFactory(_entityFrameworkRoomieDatabaseBackend.Database.Connection),
                new EntityFrameworkRepositoryFactory(_entityFrameworkRoomieDatabaseBackend)
            );
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
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

            var sessionlessWebHookComputer = GetSessionlessWebHookComputer(controllerContext.Request);

            if (sessionlessWebHookComputer != null)
            {
                Computer = sessionlessWebHookComputer;
            }

            base.Initialize(controllerContext);
        }

        protected void AddTask(Persistence.Models.Computer computer, string origin, string scriptText)
        {
            var scriptRepository = RepositoryFactory.GetScriptRepository();
            var taskRepository = RepositoryFactory.GetTaskRepository();

            var script = Persistence.Models.Script.Create(false, scriptText);
            scriptRepository.Add(script);

            var task = Persistence.Models.Task.Create(User, origin, computer, script);
            taskRepository.Add(task);
        }

        private Persistence.Models.UserSession GetCurrentUserSession(HttpRequestMessage request)
        {
            var sessionRepository = RepositoryFactory.GetSessionRepository();

            var cookie = request.Headers.GetCookies(SessionTokenCookieName)
                .FirstOrDefault();

            if (cookie == null)
            {
                return null;
            }

            var token = cookie[SessionTokenCookieName].Value;

            var session = sessionRepository.GetUserSession(token);

            if (session == null)
            {
                return null;
            }

            session.UpdateLastContact();
            sessionRepository.Update(session);

            return session;
        }

        private Persistence.Models.WebHookSession GetCurrentWebHookSession(HttpRequestMessage request)
        {
            var sessionRepository = RepositoryFactory.GetSessionRepository();
            var computerRepository = RepositoryFactory.GetComputerRepository();

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

            var session = sessionRepository.GetWebHookSession(token);

            if (session == null)
            {
                return null;
            }

            session.Computer.UpdatePing();
            computerRepository.Update(session.Computer);

            return session;
        }

        private Persistence.Models.Computer GetSessionlessWebHookComputer(HttpRequestMessage request)
        {
            var computerRepository = RepositoryFactory.GetComputerRepository();

            if (!request.Headers.Contains(WebHookAccessKeyHeaderName))
            {
                return null;
            }

            var accessKey = request.Headers.GetValues(WebHookAccessKeyHeaderName)
                .FirstOrDefault();

            if (accessKey == null)
            {
                return null;
            }

            var computer = computerRepository.Get(accessKey);

            return computer;
        }

        protected override void Dispose(bool disposing)
        {
            _entityFrameworkRoomieDatabaseBackend.Dispose();
            base.Dispose(disposing);
        }
    }
}