using System.Collections.Generic;

namespace Roomie.Common.HomeAutomation
{
    public interface INetworkActions
    {
        IEnumerable<IDeviceActions> DeviceActions { get; }
    }
}
