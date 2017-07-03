
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface ISessionRepository
    {
        UserSession GetUserSession(string token);
        WebHookSession GetWebHookSession(string token);
        void Add(UserSession session);
        void Add(WebHookSession session);
        void Update(UserSession session);
        Page<UserSession> ListUserSessions(User user, ListFilter filter);
        Page<WebHookSession> ListWebhookSessions(User user, ListFilter filter);
    }
}
