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

        public override bool ProcessValueChanged(OpenZWaveDeviceValue entry)
        {
            if (!Matches(entry))
            {
                return false;
            }

            var @event = DeviceEvent.HumiditySensorValueChanged(Device, null);
            Device.AddEvent(@event);

            return true;
        }
    }
}
