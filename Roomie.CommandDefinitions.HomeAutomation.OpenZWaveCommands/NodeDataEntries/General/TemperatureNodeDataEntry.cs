using Roomie.Common.Measurements.Temperature;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General
{
    public abstract class TemperatureNodeDataEntry : NodeDataEntry<ITemperature>
    {
        protected TemperatureNodeDataEntry(OpenZWaveDevice device, CommandClass commandClass, byte? index = null)
            : base(device, commandClass, index)
        {
        }

        public override ITemperature GetValue()
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                return null;
            }

            var number = dataEntry.DecimalValue ?? 0;
            var units = dataEntry.Units;

            if (number == 0 && units == string.Empty)
            {
                return null;
            }

            var result = TemperatureParser.Parse((double) number, units);

            return result;
        }
    }
}
