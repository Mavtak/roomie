using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public static class Helpers
    {
        public static void AddUserAndCopyAccess(IUserRepository userRepository, INetworkRepository networkRepository, INetworkGuestRepository networkGuestRepository, string username, string password, string referenceUsername)
        {
            var user = User.Create(string.Join(":", "internal", username));
            user.Secret = Web.Persistence.Helpers.Secrets.BCryptSecret.FromPassword(password);

            userRepository.Add(user);

            var referenceUser = userRepository.Get(string.Join(":", "internal", referenceUsername));

            var networks = networkRepository.Get(referenceUser);

            foreach (var network in networks)
            {
                networkGuestRepository.Add(network, user);
            }
        }
    }
}
