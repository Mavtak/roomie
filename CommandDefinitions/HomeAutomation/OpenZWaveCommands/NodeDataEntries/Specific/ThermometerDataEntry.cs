using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General;
using Roomie.Common.HomeAutomation.Events;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific
{
    public class ThermometerDataEntry : TemperatureNodeDataEntry
    {
        public ThermometerDataEntry(OpenZWaveDevice device)
            : base(device, (byte)CommandClass.SensorMultilevel)
        {
        }

        public override bool ProcessValueChanged(OpenZWaveDeviceValue entry)
        {
            if (entry.CommandClass != CommandClass.SwitchMultilevel)
            {
                return false;
            }

            var @event = DeviceEvent.TemperatureChanged(Device, null);
            Device.AddEvent(@event);

            return true;
        }
    }
}
