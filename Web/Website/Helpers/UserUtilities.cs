using System;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Security;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.Helpers
{
    public class UserUtilities
    {
        private static string sessionTokenName = "roomie_session";

        public static UserSession GetCurrentUserSession(IRoomieDatabaseContext database)
        {
            var request = HttpContext.Current.Request;
            if (request.Cookies[sessionTokenName] == null)
            {
                return null;
            }

            var token = request.Cookies[sessionTokenName].Value;

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

        public static UserSession CreateSession(IRoomieDatabaseContext database, User user)
        {
            var userSession = UserSession.Create(user);

            database.Sessions.Add(userSession);

            return userSession;
        }

        private static CookieHeaderValue CreateSessionCookie(string token, DateTime expires)
        {
            return new CookieHeaderValue(sessionTokenName, token)
            {
                Expires = expires,
                HttpOnly = true,
                Path = "/"
            };
        }

        public static CookieHeaderValue CreateNewSessionCookie(UserSession userSession)
        {
            return CreateSessionCookie(userSession.Token, DateTime.UtcNow.AddYears(1));
        }

        public static CookieHeaderValue CreateExpiredSessionCookie()
        {
            return CreateSessionCookie("expired", DateTime.UtcNow.AddYears(-10));
        }
    }
}