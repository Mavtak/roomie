using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly DbSet<UserSessionModel> _userSessions;
        private readonly DbSet<WebHookSessionModel> _webHookSessions;

        public SessionRepository(DbSet<UserSessionModel> userSessions, DbSet<WebHookSessionModel> webHookSessions)
        {
            _userSessions = userSessions;
            _webHookSessions = webHookSessions;
        }

        public UserSessionModel GetUserSession(string token)
        {
            var result = _userSessions.FirstOrDefault(x => x.Token == token);

            return result;
        }

        public WebHookSessionModel GetWebHookSession(string token)
        {
            var result = _webHookSessions.FirstOrDefault(x => x.Token == token);

            return result;
        }

        public void Add(UserSessionModel session)
        {
            _userSessions.Add(session);
        }

        public void Add(WebHookSessionModel session)
        {
            _webHookSessions.Add(session);
        }

        Page<UserSessionModel> ISessionRepository.ListUserSessions(UserModel user, ListFilter filter)
        {
            var results = (from x in _userSessions
                           where x.User.Id == user.Id
                           select x).Page(filter, x => x.Id)
                           ;

            return results;
        }

        Page<WebHookSessionModel> ISessionRepository.ListWebhookSessions(UserModel user, ListFilter filter)
        {
            var results = (from x in _webHookSessions
                           where x.Computer.Owner.Id == user.Id
                           select x).Page(filter, x => x.Id)
                           ;

            return results;
        }
    }
}
