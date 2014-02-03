using System;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats.Fans;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific
{
    class ThermostatFanCurrentActionDataEntry : NodeDataEntry<ThermostatFanCurrentAction?>
    {
        public ThermostatFanCurrentActionDataEntry(OpenZWaveDevice device)
            : base(device, CommandClass.ThermostatFanState)
        {
        }

        public override ThermostatFanCurrentAction? GetValue()
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                return null;
            }

            var stringValue = dataEntry.StringValue;
            stringValue = NormalizeStringValue(stringValue);

            var result = ThermostatFanCurrentActionParser.Parse(stringValue);

            return result;
        }

        public override bool ProcessValueChanged(OpenZWaveDeviceValue entry)
        {
            if (!Matches(entry))
            {
                return false;
            }

            var @event = DeviceEvent.ThermostatFanCurrentActionChanged(Device, null);
            Device.AddEvent(@event);

            return true;
        }

        private static string NormalizeStringValue(string value)
        {
            //TODO: move this into the parser?
            if (value.Equals("Running", StringComparison.InvariantCultureIgnoreCase))
            {
                value = ThermostatFanCurrentAction.On.ToString();
            }

            return value;
        }
    }
}
