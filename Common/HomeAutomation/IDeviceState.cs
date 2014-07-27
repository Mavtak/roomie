using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.Measurements.Humidity;
using Roomie.Common.Measurements.Illuminance;
using Roomie.Common.Measurements.Power;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.Common.HomeAutomation
{
    public interface IDeviceState : IHasName
    {
        //TODO: change to NetworkAddress
        string Address { get; }
        ILocation Location { get; }
        //TODO: convert to interface
        INetworkState NetworkState { get; }
        bool? IsConnected { get; }
        DeviceType Type { get; }

        string CurrentAction { get; }
        
        IBinarySwitchState BinarySwitchState { get; }
        IMultilevelSensorState<IPower> PowerSensorState { get; }
        IMultilevelSensorState<ITemperature> TemperatureSensorState { get; }
        IMultilevelSensorState<IHumidity> HumiditySensorState { get; }
        IMultilevelSensorState<IIlluminance> IlluminanceSensorState { get; }
        IMultilevelSwitchState MultilevelSwitchState { get; }
        IBinarySensorState BinarySensorState { get; }
        IThermostatState ThermostatState { get; }
        IKeypadState KeypadState { get; }
    }
}
