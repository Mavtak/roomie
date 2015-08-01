
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface ISessionRepository
    {
        EntityFrameworkUserSessionModel GetUserSession(string token);
        EntityFrameworkWebHookSessionModel GetWebHookSession(string token);
        void Add(EntityFrameworkUserSessionModel session);
        void Add(EntityFrameworkWebHookSessionModel session);
        Page<EntityFrameworkUserSessionModel> ListUserSessions(EntityFrameworkUserModel user, ListFilter filter);
        Page<EntityFrameworkWebHookSessionModel> ListWebhookSessions(EntityFrameworkUserModel user, ListFilter filter);
    }
}
