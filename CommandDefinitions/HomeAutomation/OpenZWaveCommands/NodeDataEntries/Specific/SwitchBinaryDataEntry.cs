using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General;
using Roomie.Common.HomeAutomation.Events;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific
{
    public class SwitchBinaryDataEntry : BoolNodeDataEntry
    {
        public SwitchBinaryDataEntry(OpenZWaveDevice device)
            : base(device, CommandClass.SwitchBinary)
        {
        }

        protected override IDeviceEvent CreateDeviceEvent()
        {
            var state = GetValue();

            if (state == true)
            {
                return DeviceEvent.PoweredOn(Device, null);
            }

            if (state == false)
            {
                return DeviceEvent.PoweredOff(Device, null);
            }

            return DeviceEvent.PowerChanged(Device, null);
        }
    }
}
