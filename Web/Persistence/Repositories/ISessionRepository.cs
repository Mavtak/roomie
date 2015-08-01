
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface ISessionRepository
    {
        UserSession GetUserSession(string token);
        WebHookSession GetWebHookSession(string token);
        void Add(UserSession session);
        void Add(WebHookSession session);
        Page<UserSession> ListUserSessions(EntityFrameworkUserModel user, ListFilter filter);
        Page<WebHookSession> ListWebhookSessions(EntityFrameworkUserModel user, ListFilter filter);
    }
}
