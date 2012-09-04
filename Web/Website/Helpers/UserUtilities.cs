using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using Roomie.Web.Models;
using Roomie.Web.Helpers;


namespace Roomie.Web.Website.Helpers
{
    public class UserUtilities
    {
        private static string sessionTokenName = "roomie_session";

        private static UserModel GetUserByToken(RoomieDatabaseContext database, string token)
        {
            return (from r in database.Users
                    where r.Token == token
                    select r).FirstOrDefault();
        }

        public static UserSessionModel GetCurrentUserSession(RoomieDatabaseContext database)
        {
            var request = HttpContext.Current.Request;
            if (request.Cookies[sessionTokenName] == null)
            {
                return null;
            }

            var token = request.Cookies[sessionTokenName].Value;

            //TODO: use database.UserSessions.Find()
            var session = (from s in database.UserSessions
                           where s.Token == token
                           select s).FirstOrDefault();

            if (session == null)
            {
                return null;
            }

            //TODO: remove this hack
            if (session.User == null)
            {
                var hack = database.Users.ToList();
            }

            session.LastContactTimeStamp = DateTime.UtcNow;

            return session;
        }

        public static UserModel GetCurrentUser(RoomieDatabaseContext database)
        {
            var session = GetCurrentUserSession(database);
            if (session == null)
                return null;

            return session.User;
        }

        public static void SignIn(RoomieDatabaseContext database, DotNetOpenAuth.OpenId.Identifier identifier)
        {
            var response = HttpContext.Current.Response;

            var token = "openid:" + identifier;

            var user = GetUserByToken(database, token);

            if (user == null)
            {
                // add new user
                user = new UserModel
                {
                    RegisteredTimestamp = DateTime.UtcNow,
                    Token = token,
                    IsAdmin = token.Equals("openid:http://davidmcgrath.com/")
                };

                database.Users.Add(user);
                database.SaveChanges();
            }

            // create session
            var userSession = new UserSessionModel
            {
                User = user
            };
            database.UserSessions.Add(userSession);

            // set session cookie
            var cookie = new HttpCookie(sessionTokenName, userSession.Token);
            cookie.Expires = DateTime.UtcNow.AddYears(1);
            response.SetCookie(cookie);
            
            database.SaveChanges();
        }

        public static void SignOff()
        {
            var response = HttpContext.Current.Response;

            // remove session cookie
            var cookie = new HttpCookie(sessionTokenName, "expired");
            cookie.Expires = DateTime.UtcNow.AddYears(-10);
            response.SetCookie(cookie);

            FormsAuthentication.SignOut();
            
        }
    }
}