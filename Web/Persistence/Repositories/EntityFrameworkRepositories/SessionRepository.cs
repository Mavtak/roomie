using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly DbSet<EntityFrameworkUserSessionModel> _userSessions;
        private readonly DbSet<EntityFrameworkWebHookSessionModel> _webHookSessions;

        public SessionRepository(DbSet<EntityFrameworkUserSessionModel> userSessions, DbSet<EntityFrameworkWebHookSessionModel> webHookSessions)
        {
            _userSessions = userSessions;
            _webHookSessions = webHookSessions;
        }

        public UserSession GetUserSession(string token)
        {
            var model = _userSessions.FirstOrDefault(x => x.Token == token);

            if (model == null)
            {
                return null;
            }

            return model.ToRepositoryType();
        }

        public WebHookSession GetWebHookSession(string token)
        {
            var model = _webHookSessions.FirstOrDefault(x => x.Token == token);

            if (model == null)
            {
                return null;
            }

            return model.ToRepositoryType();
        }

        public void Add(UserSession session)
        {
            var model = EntityFrameworkUserSessionModel.FromRepositoryType(session);

            _userSessions.Add(model);
        }

        public void Add(WebHookSession session)
        {
            var model = EntityFrameworkWebHookSessionModel.FromRepositoryType(session);

            _webHookSessions.Add(model);
        }

        Page<UserSession> ISessionRepository.ListUserSessions(EntityFrameworkUserModel user, ListFilter filter)
        {
            var results = (from x in _userSessions
                           where x.User.Id == user.Id
                           select x.ToRepositoryType()).Page(filter, x => x.Id)
                           ;

            return results;
        }

        Page<WebHookSession> ISessionRepository.ListWebhookSessions(EntityFrameworkUserModel user, ListFilter filter)
        {
            var results = (from x in _webHookSessions
                           where x.Computer.Owner.Id == user.Id
                           select x.ToRepositoryType()).Page(filter, x => x.Id)
                           ;

            return results;
        }
    }
}
