
using System.Collections.Generic;

namespace Roomie.Common.HomeAutomation
{
    public interface INetworkState : IHasName
    {
        string Address { get; }
        IEnumerable<IDeviceState> DeviceStates { get; }
    }
}
