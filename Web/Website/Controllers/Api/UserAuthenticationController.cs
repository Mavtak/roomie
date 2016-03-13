﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api
{
    public class UserAuthenticationController : RoomieBaseApiController
    {
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

            var session = UserUtilities.CreateSession(Database, user);
            var cookie = UserUtilities.CreateSessionCookie(session);

            var response = new HttpResponseMessage();
            response.Headers.AddCookies(new[] {cookie});

            return response;
        }

        public HttpResponseMessage Delete()
        {
            var cookie = UserUtilities.ExpireSessionCookie();

            var response = new HttpResponseMessage();
            response.Headers.AddCookies(new[] {cookie});

            return response;
        }
    }
}