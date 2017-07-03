using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General;
using Roomie.Common.HomeAutomation.Events;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific
{
    public class SwitchMultilevelDataEntry : ByteNodeDataEntry
    {
        public SwitchMultilevelDataEntry(OpenZWaveDevice device)
            : base(device, CommandClass.SwitchMultilevel)
        {
        }

        protected override IDeviceEvent CreateDeviceEvent()
        {
            return DeviceEvent.PowerChanged(Device, null);
        }
    }
}