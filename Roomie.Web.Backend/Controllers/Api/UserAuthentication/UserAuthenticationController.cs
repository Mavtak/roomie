﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Backend.Helpers;

namespace Roomie.Web.Backend.Controllers.Api.UserAuthentication
{
    public class UserAuthenticationController : BaseController
    {
        private ISessionRepository _sessionRepository;
        private IUserRepository _userRepository;

        public UserAuthenticationController()
        {
            _sessionRepository = RepositoryFactory.GetSessionRepository();
            _userRepository = RepositoryFactory.GetUserRepository();
        }

        [ApiRestrictedAccess]
        public void Get()
        {
        }

        public HttpResponseMessage Post(string username, string password)
        {
            var user = _userRepository.Get(username, password);

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

            var session = Persistence.Models.UserSession.Create(user);

            _sessionRepository.Add(session);

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
            return new CookieHeaderValue(SessionTokenCookieName, token)
            {
                Expires = expires,
                HttpOnly = true,
                Path = "/"
            };
        }
    }
}