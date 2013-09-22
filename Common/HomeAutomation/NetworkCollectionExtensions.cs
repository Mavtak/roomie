using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.Common.HomeAutomation
{
    public static class NetworkCollectionExtensions
    {
        public static bool Contains(this IEnumerable<INetwork> networks, string name)
        {
            var result = networks.Any(x => x.Name.Equals(name));

            return result;
        }

        public static T GetNetwork<T>(this IEnumerable<T> networks, string name)
            where T : INetwork
        {
            var query = from network in networks
                        where network.Name == name
                        select network;

            if (!query.Any())
            {
                throw new NoMatchingNetworkException(name);
            }

            if (query.Count() > 1)
            {
                throw new MultipleMatchingNetworksException(name, query.Cast<INetwork>());
            }
            
            return query.First();
        }
    }
}
