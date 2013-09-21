using System.Collections.Generic;

namespace Roomie.Common.HomeAutomation
{
    public class ReadOnlyNetworkState : INetworkState
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public IEnumerable<IDeviceState> DeviceStates { get; private set; }
    }
}
