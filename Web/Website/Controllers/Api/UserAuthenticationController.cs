using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api
{
    public class UserAuthenticationController : RoomieBaseApiController
    {
        [ApiRestrictedAccess]
        public void Get()
        {
        }

        public HttpResponseMessage Post(string username, string password)
        {
            var user = Database.Users.Get(username, password);

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new[]
                {
                    new Error
                    {
                        FriendlyMessage = "User not found",
                        Type = "not-found",
                    }
                });
            }

            var session = UserSession.Create(user);

            Database.Sessions.Add(session);

            var cookie = CreateSessionCookie(session.Token, DateTime.UtcNow.AddYears(1));

            var response = new HttpResponseMessage();
            response.Headers.AddCookies(new[] {cookie});

            return response;
        }

        public HttpResponseMessage Delete()
        {
            var cookie = CreateSessionCookie("expired", DateTime.UtcNow.AddYears(-10));

            var response = new HttpResponseMessage();
            response.Headers.AddCookies(new[] {cookie});

            return response;
        }

        private static CookieHeaderValue CreateSessionCookie(string token, DateTime expires)
        {
            return new CookieHeaderValue(SessionTokenName, token)
            {
                Expires = expires,
                HttpOnly = true,
                Path = "/"
            };
        }
    }
}