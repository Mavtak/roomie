using System;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories
{
    public class SessionRepository : ISessionRepository
    {
        private UserSessionRepository _userSessionRepository;
        private WebHookSessionRepository _webHookSessionRepository;

        public SessionRepository(UserSessionRepository userSessionRepository, WebHookSessionRepository webHookSessionRepository)
        {
            _userSessionRepository = userSessionRepository;
            _webHookSessionRepository = webHookSessionRepository;
        }

        public void Add(WebHookSession session)
        {
            _webHookSessionRepository.Add(session);
        }

        public void Add(UserSession session)
        {
            _userSessionRepository.Add(session);
        }

        public UserSession GetUserSession(string token, IRepositoryModelCache cache = null)
        {
            return _userSessionRepository.Get(token, cache);
        }

        public WebHookSession GetWebHookSession(string token, IRepositoryModelCache cache = null)
        {
            return _webHookSessionRepository.Get(token, cache);
        }

        public Page<UserSession> ListUserSessions(User user, ListFilter filter, IRepositoryModelCache cache = null)
        {
            return _userSessionRepository.List(user, filter, cache);
        }

        public Page<WebHookSession> ListWebhookSessions(User user, ListFilter filter, IRepositoryModelCache cache = null)
        {
            return _webHookSessionRepository.List(user, filter, cache);
        }

        public void Update(UserSession session)
        {
            _userSessionRepository.Update(session);
        }
    }
}
