using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public interface IThermostatState
    {
        ITemperature Temperature { get; }
    }
}
