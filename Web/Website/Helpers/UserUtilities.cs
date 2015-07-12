﻿using System;
using System.Web;
using System.Web.Security;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.Helpers
{
    public class UserUtilities
    {
        private static string sessionTokenName = "roomie_session";

        public static UserSessionModel GetCurrentUserSession(IRoomieDatabaseContext database)
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

            session.LastContactTimeStamp = DateTime.UtcNow;

            return session;
        }

        public static UserModel GetCurrentUser(IRoomieDatabaseContext database)
        {
            var session = GetCurrentUserSession(database);
            if (session == null)
                return null;

            return session.User;
        }

        public static void SignIn(IRoomieDatabaseContext database, DotNetOpenAuth.OpenId.Identifier identifier)
        {
            var response = HttpContext.Current.Response;

            var token = "openid:" + identifier;

            var user = database.Users.Get(token);

            if (user == null)
            {
                // add new user
                user = new UserModel
                {
                    RegisteredTimestamp = DateTime.UtcNow,
                    Token = token
                };

                database.Users.Add(user);
                database.SaveChanges();
            }

            var session = CreateSession(database, user);
            var cookie = CreateSessionCookie(session);
            response.SetCookie(cookie);
            database.SaveChanges();
        }

        public static UserSessionModel CreateSession(IRoomieDatabaseContext database, UserModel user)
        {
            var userSession = new UserSessionModel
            {
                User = user
            };

            database.Sessions.Add(userSession);

            return userSession;
        }

        public static HttpCookie CreateSessionCookie(UserSessionModel userSession)
        {
            var cookie = new HttpCookie(sessionTokenName, userSession.Token);
            cookie.Expires = DateTime.UtcNow.AddYears(1);

            return cookie;
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