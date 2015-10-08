using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly DbSet<EntityFrameworkUserSessionModel> _userSessions;
        private readonly DbSet<EntityFrameworkWebHookSessionModel> _webHookSessions;
        private readonly DbSet<EntityFrameworkComputerModel> _computers;
        private readonly DbSet<EntityFrameworkUserModel> _users;

        public SessionRepository(DbSet<EntityFrameworkUserSessionModel> userSessions, DbSet<EntityFrameworkWebHookSessionModel> webHookSessions, DbSet<EntityFrameworkComputerModel>  computers, DbSet<EntityFrameworkUserModel> users)
        {
            _userSessions = userSessions;
            _webHookSessions = webHookSessions;
            _computers = computers;
            _users = users;
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
            var model = EntityFrameworkUserSessionModel.FromRepositoryType(session, _users);

            _userSessions.Add(model);
        }

        public void Add(WebHookSession session)
        {
            var model = EntityFrameworkWebHookSessionModel.FromRepositoryType(session, _computers);

            _webHookSessions.Add(model);
        }

        public void Update(UserSession session)
        {
            var model = _userSessions.Find(session.Id);

            model.LastContactTimeStamp = session.LastContactTimeStamp;
        }

        Page<UserSession> ISessionRepository.ListUserSessions(User user, ListFilter filter)
        {
            var results = (from x in _userSessions
                           where x.User.Id == user.Id
                           select x.ToRepositoryType()).Page(filter, x => x.Id)
                           ;

            return results;
        }

        Page<WebHookSession> ISessionRepository.ListWebhookSessions(User user, ListFilter filter)
        {
            var results = (from x in _webHookSessions
                           where x.Computer.Owner.Id == user.Id
                           select x.ToRepositoryType()).Page(filter, x => x.Id)
                           ;

            return results;
        }
    }
}
