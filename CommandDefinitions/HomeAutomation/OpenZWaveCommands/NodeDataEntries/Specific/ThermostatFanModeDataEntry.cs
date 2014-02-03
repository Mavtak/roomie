using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats.Fans;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific
{
    public class ThermostatFanModeDataEntry : NodeDataEntry<ThermostatFanMode?>,
        IWritableNodeDataEntry<ThermostatFanMode>,
        INodeDataEntryWithOptions<ThermostatFanMode?>
    {
        public ThermostatFanModeDataEntry(OpenZWaveDevice device)
            : base(device, CommandClass.ThermostatFanMode)
        {
        }

        public IEnumerable<ThermostatFanMode?> GetOptions()
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                return new ThermostatFanMode?[0];
            }

            var stringValues = dataEntry.ListItemsValue;

            var result = stringValues
                .Select(NormalizeStringValue)
                .Where(ThermostatFanModeParser.IsValid) //TODO: other modes?
                .Select(ThermostatFanModeParser.Parse)
                .Select(x => new ThermostatFanMode?(x));

            return result.ToArray();
        }

        public override ThermostatFanMode? GetValue()
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                return null;
            }

            var stringValue = dataEntry.Selection;
            stringValue = NormalizeStringValue(stringValue);

            var result = ThermostatFanModeParser.Parse(stringValue);

            return result;
        }

        public void SetValue(ThermostatFanMode value)
        {
            var dataEntry = GetDataEntry();
            var stringValue = value.ToString() + " Low"; //TODO: support high values?

            dataEntry.SetSelection(stringValue);
        }

        public override bool ProcessValueChanged(OpenZWaveDeviceValue entry)
        {
            if (entry.CommandClass != CommandClass.ThermostatFanMode)
            {
                return false;
            }

            var @event = DeviceEvent.ThermostatFanModeChanged(Device, null);
            Device.AddEvent(@event);

            return true;
        }

        private static string NormalizeStringValue(string value)
        {
            //TODO: support the difference between "Auto Low" and "Auto High"?
            if (value.Contains(' '))
            {
                value = value.Substring(0, value.IndexOf(' '));
            }

            return value;
        }
    }
}
