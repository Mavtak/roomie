using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General;
using Roomie.Common.HomeAutomation.Events;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific
{
    public class ImmediatePowerDataEntry : DecimalNodeDataEntry
    {
        public ImmediatePowerDataEntry(OpenZWaveDevice device)
            : base(device, CommandClass.Meter, 8)
        {
        }

        protected override IDeviceEvent CreateDeviceEvent()
        {
            return DeviceEvent.PowerSensorValueChanged(Device, null);
        }
    }
}
