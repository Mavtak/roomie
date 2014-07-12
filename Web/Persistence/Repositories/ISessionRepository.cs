
using Roomie.Web.Persistence.Models;
namespace Roomie.Web.Persistence.Repositories
{
    public interface ISessionRepository
    {
        UserSessionModel GetUserSession(string token);
        WebHookSessionModel GetWebHookSession(string token);
        void Add(UserSessionModel session);
        void Add(WebHookSessionModel session);
        Page<UserSessionModel> ListUserSessions(UserModel user, ListFilter filter);
        Page<WebHookSessionModel> ListWebhookSessions(UserModel user, ListFilter filter);
    }
}
