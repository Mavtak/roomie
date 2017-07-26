
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface ISessionRepository
    {
        UserSession GetUserSession(string token, IRepositoryModelCache cache = null);
        WebHookSession GetWebHookSession(string token, IRepositoryModelCache cache = null);
        void Add(UserSession session);
        void Add(WebHookSession session);
        void Update(UserSession session);
        Page<UserSession> ListUserSessions(User user, ListFilter filter, IRepositoryModelCache cache = null);
        Page<WebHookSession> ListWebhookSessions(User user, ListFilter filter, IRepositoryModelCache cache = null);
    }
}
