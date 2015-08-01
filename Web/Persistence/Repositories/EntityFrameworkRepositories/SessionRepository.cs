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

        public EntityFrameworkUserSessionModel GetUserSession(string token)
        {
            var result = _userSessions.FirstOrDefault(x => x.Token == token);

            return result;
        }

        public EntityFrameworkWebHookSessionModel GetWebHookSession(string token)
        {
            var result = _webHookSessions.FirstOrDefault(x => x.Token == token);

            return result;
        }

        public void Add(EntityFrameworkUserSessionModel session)
        {
            _userSessions.Add(session);
        }

        public void Add(EntityFrameworkWebHookSessionModel session)
        {
            _webHookSessions.Add(session);
        }

        Page<EntityFrameworkUserSessionModel> ISessionRepository.ListUserSessions(EntityFrameworkUserModel user, ListFilter filter)
        {
            var results = (from x in _userSessions
                           where x.User.Id == user.Id
                           select x).Page(filter, x => x.Id)
                           ;

            return results;
        }

        Page<EntityFrameworkWebHookSessionModel> ISessionRepository.ListWebhookSessions(EntityFrameworkUserModel user, ListFilter filter)
        {
            var results = (from x in _webHookSessions
                           where x.Computer.Owner.Id == user.Id
                           select x).Page(filter, x => x.Id)
                           ;

            return results;
        }
    }
}
