﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api
{
    [AutoSave]
    public class UserAuthenticationController : RoomieBaseApiController
    {
        public HttpResponseMessage Post(string username, string password)
        {
            var response = new HttpResponseMessage();
            var user = Database.Users.Get(username, password);

            if (user == null)
            {
                throw new Exception();
            }

            var session = UserUtilities.CreateSession(Database, user);
            var cookie = UserUtilities.CreateSessionCookie(session);
            SetCookie(response, cookie.Name, cookie.Value, cookie.Expires);

            return response;
        }

        private static void SetCookie(HttpResponseMessage response, string name, string value, DateTime expires)
        {
            response.Headers.AddCookies(new[]
            {
                new CookieHeaderValue(name, value)
                {
                    Expires = expires,
                    HttpOnly = true,
                    Path = "/"
                }
            });
        }
    }
}