using Roomie.CommandDefinitions.OpenZWaveCommands.OpenZWaveDeviceValueMatchers;
using Roomie.Common.Measurements;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General
{
    public abstract class MeasurementNodeDataEntry<TMeasurement> : NodeDataEntry<TMeasurement>
        where TMeasurement : class, IMeasurement
    {
        protected MeasurementNodeDataEntry(OpenZWaveDevice device, IOpenZWaveDeviceValueMatcher unitsMatcher)
            : base(device, CreateMatcher(device, unitsMatcher))
        {
        }

        public override TMeasurement GetValue()
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                return null;
            }

            var number = dataEntry.DecimalValue.Value;
            var units = dataEntry.Units;

            if (number == 0 && units == string.Empty)
            {
                return null;
            }

            var result = MeasurementParser.Parse<TMeasurement>((double)number, units);

            return result;
        }

        private static IOpenZWaveDeviceValueMatcher CreateMatcher(OpenZWaveDevice device, IOpenZWaveDeviceValueMatcher unitsMatcher)
        {
            var result = new CompositeMatcher(
                new IdMatcher(device.Id),
                new CommandClassMatcher(CommandClass.SensorMultilevel),
                unitsMatcher
                );

            return result;
        }
    }
}
