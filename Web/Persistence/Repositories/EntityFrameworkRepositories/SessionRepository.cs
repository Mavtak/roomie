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

        UserSessionModel[] ISessionRepository.ListUserSessions(UserModel user, ListFilter filter)
        {
            var results = (from x in _userSessions
                           where x.User.Id == user.Id
                           orderby x.Id descending
                           select x).Page(filter)
                                    .ToArray();

            return results;
        }

        WebHookSessionModel[] ISessionRepository.ListWebhookSessions(UserModel user, ListFilter filter)
        {
            var results = (from x in _webHookSessions
                           where x.Computer.Owner.Id == user.Id
                           orderby x.Id descending
                           select x).Page(filter)
                                    .ToArray();

            return results;
        }
    }
}
