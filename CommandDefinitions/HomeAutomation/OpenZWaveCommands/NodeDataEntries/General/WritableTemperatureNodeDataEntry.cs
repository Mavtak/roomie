using System;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General
{
    public abstract class WritableTemperatureNodeDataEntry : TemperatureNodeDataEntry,
        IWritableNodeDataEntry<ITemperature>
    {
        protected WritableTemperatureNodeDataEntry(OpenZWaveDevice device, CommandClass commandClass, byte index)
            : base(device, commandClass, index)
        {
        }

        public void SetValue(ITemperature value)
        {
            var dataEntry = GetDataEntry();

            double number;
            switch (dataEntry.Units)
            {
                case "F":
                    number = value.Fahrenheit.Value;
                    break;

                case "C":
                default: //TODO: error on default case?
                    number = value.Celsius.Value;
                    break;
            }

            // is this necessary?
            number = Math.Round(number);

            dataEntry.SetValue(number);
        }
    }
}
