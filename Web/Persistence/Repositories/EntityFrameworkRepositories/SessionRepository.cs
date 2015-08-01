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

            var result = ToRepositoryType(model);

            return result;
        }

        public EntityFrameworkWebHookSessionModel GetWebHookSession(string token)
        {
            var result = _webHookSessions.FirstOrDefault(x => x.Token == token);

            return result;
        }

        public void Add(UserSession session)
        {
            var model = ToEntityFrameworkType(session);

            _userSessions.Add(model);
        }

        public void Add(EntityFrameworkWebHookSessionModel session)
        {
            _webHookSessions.Add(session);
        }

        Page<UserSession> ISessionRepository.ListUserSessions(EntityFrameworkUserModel user, ListFilter filter)
        {
            var results = (from x in _userSessions
                           where x.User.Id == user.Id
                           select ToRepositoryType(x)).Page(filter, x => x.Id)
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

        private static EntityFrameworkUserSessionModel ToEntityFrameworkType(UserSession model)
        {
            var result = new EntityFrameworkUserSessionModel
            {
                CreationTimeStamp = model.CreationTimeStamp,
                Id = model.Id,
                LastContactTimeStamp = model.LastContactTimeStamp,
                Token = model.Token,
                User = model.User
            };

            return result;
        }

        private static UserSession ToRepositoryType(EntityFrameworkUserSessionModel model)
        {
            var result = new UserSession(
                creationTimeStamp: model.CreationTimeStamp,
                id: model.Id,
                lastContactTimeStamp: model.LastContactTimeStamp,
                token: model.Token,
                user: model.User
            );

            return result;
        }
    }
}
