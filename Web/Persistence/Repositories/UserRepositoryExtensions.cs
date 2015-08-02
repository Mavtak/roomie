﻿using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public static class UserRepositoryExtensions
    {
        public static void Add(this IUserRepository repository, string username, string password)
        {
            var user = User.CreateInternal(username, password);

            repository.Add(user);
        }

        public static User Get(this IUserRepository repository, string username, string password)
        {
            var token = BuildInternalUserToken(username);
            var result = repository.Get(token);

            if (result == null)
            {
                return null;
            }

            var secret = result.ParseSecret();

            if (secret == null)
            {
                return null;
            }

            var verified = secret.Verify(password);

            if (!verified)
            {
                return null;
            }

            return result;
        }

        public static string BuildInternalUserToken(string username)
        {
            return "internal:" + username;
        }
    }
}