using Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General;
using Roomie.CommandDefinitions.OpenZWaveCommands.OpenZWaveDeviceValueMatchers;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.Measurements.Humidity;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific
{
    public class HumiditySensorDataEntry : MeasurementNodeDataEntry<IHumidity>
    {
        public HumiditySensorDataEntry(OpenZWaveDevice device)
            : base(device, UnitsMatcher.Humidity())
        {
        }

        protected override IDeviceEvent CreateDeviceEvent()
        {
            return DeviceEvent.HumiditySensorValueChanged(Device, null);
        }
    }
}
