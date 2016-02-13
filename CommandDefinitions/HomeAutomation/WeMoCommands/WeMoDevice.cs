using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Common.HomeAutomation;
using Roomie.CommandDefinitions.WeMoCommands.Sklose;
using Roomie.Common.HomeAutomation.BinarySwitches;

namespace Roomie.CommandDefinitions.WeMoCommands
{
    public class WeMoDevice : Device
    {
        public WeMoBinarySwitch _binarySwitch { get; private set; }
        

        public WeMoDevice(WeMoNetwork network, BasicServicePortTypeClient client, string address)
            : base(network, DeviceType.BinarySwitch)
        {
            Address = address;
            _binarySwitch = new WeMoBinarySwitch(client);
        }

        public override IBinarySwitch BinarySwitch
        {
            get
            {
                return _binarySwitch;
            }
        }
    }
}
