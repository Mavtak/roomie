
using Roomie.Web.Persistence.Models;
namespace Roomie.Web.Persistence.Repositories
{
    public interface ISessionRepository
    {
        UserSessionModel GetUserSession(string token);
        WebHookSessionModel GetWebHookSession(string token);
        void Add(UserSessionModel session);
        void Add(WebHookSessionModel session);
        UserSessionModel[] ListUserSessions(UserModel user, int? page = 0, int? count = 0);
        WebHookSessionModel[] ListWebhookSessions(UserModel user, int? page = 0, int? count = 0);
    }
}
