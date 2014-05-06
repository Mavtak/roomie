using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.Measurements.Humidity;
using Roomie.Common.Measurements.Power;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.Common.HomeAutomation
{
    public interface IDevice : IDeviceState, IDeviceActions
    {
        INetwork Network { get; }
        IBinarySwitch BinarySwitch { get; }
        IMultilevelSwitch MultilevelSwitch { get; }
        IBinarySensor BinarySensor { get;}
        IMultilevelSensor<IPower> PowerSensor { get; }
        IMultilevelSensor<ITemperature> TemperatureSensor { get; }
        IMultilevelSensor<IHumidity> HumiditySensor { get; }
        IThermostat Thermostat { get; }
        IKeypad Keypad { get; }
    }
}
