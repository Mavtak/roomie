using System;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public static class UserRepositoryExtensions
    {
        public static UserModel Add(this IUserRepository users, string token)
        {
            var user = new UserModel
            {
                Token = token,
                RegisteredTimestamp = DateTime.UtcNow,
                IsAdmin = token.Equals("openid:http://davidmcgrath.com/")
            };

            users.Add(user);

            return user;
        }
    }
}
