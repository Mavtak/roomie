using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public class ReadOnlyThermostatState : IThermostatState
    {
        public ITemperature Temperature { get; private set; }

        public static ReadOnlyThermostatState CopyFrom(IThermostatState source)
        {
            var result = new ReadOnlyThermostatState
            {
                Temperature = source.Temperature
            };

            return result;
        }
    }
}
