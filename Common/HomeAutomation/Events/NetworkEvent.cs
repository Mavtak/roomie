using System;

namespace Roomie.Common.HomeAutomation.Events
{
    public class NetworkEvent : INetworkEvent
    {
        public Network Network { get; private set; }
        public IHasName Entity
        {
            get
            {
                return Network;
            }
        }
        public IEventType Type { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public IEventSource Source { get; private set; }

        private NetworkEvent(Network network, IEventType type, IEventSource source)
        {
            Network = network;
            Type = type;
            TimeStamp = DateTime.UtcNow;
            Source = source;
        }

        public static NetworkEvent Connected(Network network, IEventSource source)
        {
            var result = new NetworkEvent(network, new NetworkConnected(), source);

            return result;
        }

        public static NetworkEvent Disconnected(Network network, IEventSource source)
        {
            var result = new NetworkEvent(network, new NetworkDisconnected(), source);

            return result;
        }
    }
}
