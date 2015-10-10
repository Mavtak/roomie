using System;
using Roomie.Web.Persistence.Helpers.Secrets;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Persistence.Models
{
    public class User
    {
        public string Alias { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public DateTime? RegisteredTimestamp { get; set; }
        public ISecret Secret { get; set; }
        public string Token { get; set; }

        public User()
        {
        }

        public User(string alias, string email, int id, DateTime? registeredTimestamp, ISecret secret, string token)
        {
            Alias = alias;
            Email = email;
            Id = id;
            RegisteredTimestamp = registeredTimestamp;
            Secret = secret;
            Token = token;
        }

        public static User Create(string token)
        {
            var result = new User
            {
                RegisteredTimestamp = DateTime.UtcNow,
                Token = token
            };

            return result;
        }

        public static User CreateInternal(string username, string password)
        {
            var token = UserRepositoryExtensions.BuildInternalUserToken(username);
            var secret = BCryptSecret.FromPassword(password);

            var result = new User
            {
                Alias = username,
                Secret = secret,
                Token = token
            };

            return result;
        }
        public void SetId(int id)
        {
            if (Id != 0)
            {
                throw new ArgumentException("Id is already set");
            }

            Id = id;
        }

    }
}
