using System;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific
{
    public class ThermostatCoreCurrentActionDataEntry : NodeDataEntry<ThermostatCurrentAction?>
    {
        public ThermostatCoreCurrentActionDataEntry(OpenZWaveDevice device)
            : base(device, (byte) CommandClass.ThermostatOperatingState)
        {
        }

        public override ThermostatCurrentAction? GetValue()
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                return null;
            }

            var stringValue = dataEntry.StringValue;
            var value = ThermostatCurrentActionParser.Parse(stringValue);

            return value;
        }

        public override void SetValue(ThermostatCurrentAction? value)
        {
            throw new NotSupportedException();
        }

        public override bool ProcessValueChanged(OpenZWaveDeviceValue entry)
        {
            if (entry.CommandClass != CommandClass.ThermostatOperatingState)
            {
                return false;
            }

            var @event = DeviceEvent.ThermostatCurrentActionChanged(Device, null);
            Device.AddEvent(@event);

            return true;
        }
    }
}
