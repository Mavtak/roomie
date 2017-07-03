using System.Collections.Generic;

namespace Roomie.Common.HomeAutomation
{
    public interface INetwork : INetworkState, INetworkActions
    {
        IEnumerable<IDevice> Devices { get; }
    }
}
