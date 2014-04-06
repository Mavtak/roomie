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

        public override bool ProcessValueChanged(OpenZWaveDeviceValue entry)
        {
            if (!Matches(entry))
            {
                return false;
            }

            var @event = DeviceEvent.PowerSensorValueChanged(Device, null);

            Device.AddEvent(@event);

            return true;
        }
    }
}
