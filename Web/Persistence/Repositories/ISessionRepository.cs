
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface ISessionRepository
    {
        UserSession GetUserSession(string token);
        EntityFrameworkWebHookSessionModel GetWebHookSession(string token);
        void Add(UserSession session);
        void Add(EntityFrameworkWebHookSessionModel session);
        Page<UserSession> ListUserSessions(EntityFrameworkUserModel user, ListFilter filter);
        Page<EntityFrameworkWebHookSessionModel> ListWebhookSessions(EntityFrameworkUserModel user, ListFilter filter);
    }
}
