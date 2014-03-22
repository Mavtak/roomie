using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public class EntityFrameworkSessionRepository : ISessionRepository
    {
        private readonly DbSet<UserSessionModel> _userSessions;
        private readonly DbSet<WebHookSessionModel> _webHookSessions;

        public EntityFrameworkSessionRepository(DbSet<UserSessionModel> userSessions, DbSet<WebHookSessionModel> webHookSessions)
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
    }
}
