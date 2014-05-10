using Roomie.Common.HomeAutomation.Thermostats.Cores;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public interface IThermostatActions
    {
        IThermostatCoreActions CoreActions { get; }
        IThermostatFanActions FanActions { get; }
        IThermostatSetpointCollectionActions SetpointActions { get; }
    }
}
