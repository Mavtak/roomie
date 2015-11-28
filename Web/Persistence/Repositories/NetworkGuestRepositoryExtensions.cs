using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public static class NetworkGuestRepositoryExtensions
    {
        public static void Copy(this INetworkGuestRepository repository, Network source, Network destination)
        {
            var users = repository.Get(source);

            foreach (var user in users)
            {
                if (!repository.Check(destination, user))
                {
                    repository.Add(destination, user);
                }
            }
        }
    }
}
