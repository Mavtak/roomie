using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General;
using Roomie.CommandDefinitions.OpenZWaveCommands.OpenZWaveDeviceValueMatchers;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.Measurements.Illuminance;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific
{
    public class IlluminanceSensorDataEntry : MeasurementNodeDataEntry<IIlluminance>
    {
        public IlluminanceSensorDataEntry(OpenZWaveDevice device)
            : base(device, UnitsMatcher.Illuminance())
        {
        }

        protected override IDeviceEvent CreateDeviceEvent()
        {
            return DeviceEvent.IlluminanceSensorValueChanged(Device, null);
        }
    }
}
