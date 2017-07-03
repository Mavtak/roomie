using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General;
using Roomie.Common.HomeAutomation.Events;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific
{
    public class ThermometerDataEntry : TemperatureNodeDataEntry
    {
        public ThermometerDataEntry(OpenZWaveDevice device)
            : base(device, CommandClass.SensorMultilevel)
        {
        }

        protected override IDeviceEvent CreateDeviceEvent()
        {
            return DeviceEvent.TemperatureChanged(Device, null);
        }
    }
}
