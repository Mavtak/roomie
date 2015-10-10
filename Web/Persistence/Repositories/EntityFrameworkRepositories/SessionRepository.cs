using System;
using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly DbSet<UserSessionModel> _userSessions;
        private readonly DbSet<WebHookSessionModel> _webHookSessions;
        private readonly DbSet<ComputerModel> _computers;
        private readonly Action _save;
        private readonly DbSet<UserModel> _users;

        public SessionRepository(DbSet<UserSessionModel> userSessions, DbSet<WebHookSessionModel> webHookSessions, DbSet<ComputerModel>  computers, Action save, DbSet<UserModel> users)
        {
            _userSessions = userSessions;
            _webHookSessions = webHookSessions;
            _computers = computers;
            _save = save;
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
            var model = UserSessionModel.FromRepositoryType(session, _users);

            _userSessions.Add(model);

            _save();

            session.SetId(model.Id);
        }

        public void Add(WebHookSession session)
        {
            var model = WebHookSessionModel.FromRepositoryType(session, _computers);

            _webHookSessions.Add(model);

            _save();

            session.SetId(model.Id);
        }

        public void Update(UserSession session)
        {
            var model = _userSessions.Find(session.Id);

            model.LastContactTimeStamp = session.LastContactTimeStamp;

            _save();
        }

        Page<UserSession> ISessionRepository.ListUserSessions(User user, ListFilter filter)
        {
            var result = _userSessions
                .Where(x => x.User.Id == user.Id)
                .Page(filter, x => x.Id)
                .Transform(x => x.ToRepositoryType());

            return result;
        }

        Page<WebHookSession> ISessionRepository.ListWebhookSessions(User user, ListFilter filter)
        {
            var result = _webHookSessions
                .Where(x => x.Computer.Owner.Id == user.Id)
                .Page(filter, x => x.Id)
                .Transform(x => x.ToRepositoryType());

            return result;
        }
    }
}
