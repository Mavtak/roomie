using System;
using Roomie.Common.HomeAutomation.Thermostats;

namespace Roomie.Common.HomeAutomation.Events
{
    public class DeviceEvent : IDeviceEvent
    {
        public Device Device { get; private set; }
        public int? Power { get; private set; }
        public IThermostatState ThermostatState { get; private set; }

        public HomeAutomationEntity Entity
        {
            get
            {
                return Device;
            }
        }
        public IEventType Type { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public IEventSource Source { get; private set; }

        private DeviceEvent(Device device, IEventType type, IEventSource source, IThermostatState thermostatState = null)
        {
            Device = device;
            ThermostatState = thermostatState;
            Power = device.DimmerSwitch.Power;
            Type = type;
            TimeStamp = DateTime.UtcNow;
            Source = source;
        }

        public static DeviceEvent Found(Device device, IEventSource source)
        {
            var result = new DeviceEvent(device, new DeviceFound(), source);

            return result;
        }

        public static DeviceEvent Lost(Device device, IEventSource source)
        {
            var result = new DeviceEvent(device, new DeviceLost(), source);

            return result;
        }

        public static DeviceEvent PowerChanged(Device device, IEventSource source)
        {
            var result = new DeviceEvent(device, new DevicePowerChanged(), source);

            return result;
        }

        public static DeviceEvent MotionDetected(Device device, IEventSource source)
        {
            var result = new DeviceEvent(device, new MotionDetected(), source);

            return result;
        }

        public static DeviceEvent StillnessDetected(Device device, IEventSource source)
        {
            var result = new DeviceEvent(device, new StillnessDetected(), source);

            return result;
        }

        public static DeviceEvent TemperatureChanged(Device device, IEventSource source)
        {
            var state = ReadOnlyThermostatState.CopyFrom(device.Thermostat);
            var result = new DeviceEvent(device, new TemperatureChanged(), source, state);

            return result;
        }
    }
}
