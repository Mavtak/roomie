using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.Specific
{
    public class ThermostatModeDataEntry : NodeDataEntry<ThermostatMode?>,
        IWritableNodeDataEntry<ThermostatMode>,
        INodeDataEntryWithOptions<ThermostatMode?>
    {
        public ThermostatModeDataEntry(OpenZWaveDevice device)
            : base(device, CommandClass.ThermostatMode)
        {
        }

        public IEnumerable<ThermostatMode?> GetOptions()
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                return new ThermostatMode?[0];
            }

            var stringValues = dataEntry.ListItemsValue;

            var result = stringValues
                .Where(ThermostatModeParser.IsValid) //TODO: other modes?
                .Select(ThermostatModeParser.Parse)
                .Select(x => new ThermostatMode?(x));

            return result.ToArray();
        }

        public override ThermostatMode? GetValue()
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                return null;
            }

            var stringValue = dataEntry.Selection;
            var result = ThermostatModeParser.Parse(stringValue);

            return result;
        }

        public void SetValue(ThermostatMode value)
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                throw new CannotSetValueException();
            }

            var stringValue = value.ToString();

            dataEntry.SetSelection(stringValue);
        }

        protected override IDeviceEvent CreateDeviceEvent()
        {
            return DeviceEvent.ThermostatModeChanged(Device, null);
        }
    }
}
