using Roomie.Web.Persistence.Helpers.Secrets;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public static class UserRepositoryExtensions
    {
        public static void Add(this IUserRepository repository, string username, string password)
        {
            var token = BuildInternalUserToken(username);
            var secret = BCryptSecret.FromPassword(password);

            var user = new EntityFrameworkUserModel
            {
                Alias = username,
                Secret = secret.Format(),
                Token = token
            };

            repository.Add(user);
        }

        public static EntityFrameworkUserModel Get(this IUserRepository repository, string username, string password)
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

        private static string BuildInternalUserToken(string username)
        {
            return "internal:" + username;
        }
    }
}
