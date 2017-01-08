using System.Web;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.Helpers
{
    public class UserUtilities
    {
        public static string SessionTokenName = "roomie_session";

        public static UserSession GetCurrentUserSession(IRoomieDatabaseContext database)
        {
            var request = HttpContext.Current.Request;
            if (request.Cookies[SessionTokenName] == null)
            {
                return null;
            }

            var token = request.Cookies[SessionTokenName].Value;

            var session = database.Sessions.GetUserSession(token);

            if (session == null)
            {
                return null;
            }

            session.UpdateLastContact();
            database.Sessions.Update(session);

            return session;
        }

        public static User GetCurrentUser(IRoomieDatabaseContext database)
        {
            var session = GetCurrentUserSession(database);
            if (session == null)
                return null;

            return session.User;
        }
    }
}