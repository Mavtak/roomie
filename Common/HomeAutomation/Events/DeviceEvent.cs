using System;
using Roomie.Common.HomeAutomation.BinarySensors;

namespace Roomie.Common.HomeAutomation.Events
{
    public class DeviceEvent : IDeviceEvent
    {
        public IDevice Device { get; private set; }
        public IDeviceState State { get; private set; }

        public IHasName Entity
        {
            get
            {
                return Device;
            }
        }
        public IEventType Type { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public IEventSource Source { get; private set; }

        private DeviceEvent(IDevice device, IEventType type, IEventSource source)
        {
            Device = device;
            Type = type;
            Source = source;

            State = device.Copy();
            TimeStamp = DateTime.UtcNow;
        }

        public static DeviceEvent Found(IDevice device, IEventSource source)
        {
            var result = new DeviceEvent(device, new DeviceFound(), source);

            return result;
        }

        public static DeviceEvent Lost(IDevice device, IEventSource source)
        {
            var result = new DeviceEvent(device, new DeviceLost(), source);

            return result;
        }

        public static DeviceEvent PoweredOn(IDevice device, IEventSource source)
        {
            var result = new DeviceEvent(device, new PoweredOn(), source);

            return result;
        }

        public static DeviceEvent PoweredOff(IDevice device, IEventSource source)
        {
            var result = new DeviceEvent(device, new PoweredOff(), source);

            return result;
        }

        public static DeviceEvent PowerChanged(IDevice device, IEventSource source)
        {
            var result = new DeviceEvent(device, new DevicePowerChanged(), source);

            return result;
        }

        public static DeviceEvent BinarySensorValueChanged(IDevice device, IEventSource source)
        {
            var type = device.BinarySensor.Type;
            var value = device.BinarySensor.Value;

            switch (type)
            {
                case BinarySensorType.Motion:
                    if (value == true)
                    {
                        return MotionDetected(device, source);
                    }
                    
                    if (value == false)
                    {
                        return StillnessDetected(device, source);
                    }
                    break;
            }

            var result = new DeviceEvent(device, new BinarySensorValueChanged(), source);

            return result;
        }

        public static DeviceEvent MotionDetected(IDevice device, IEventSource source)
        {
            var result = new DeviceEvent(device, new MotionDetected(), source);

            return result;
        }

        public static DeviceEvent StillnessDetected(IDevice device, IEventSource source)
        {
            var result = new DeviceEvent(device, new StillnessDetected(), source);

            return result;
        }

        public static DeviceEvent TemperatureChanged(IDevice device, IEventSource source)
        {
            var result = new DeviceEvent(device, new TemperatureChanged(), source);

            return result;
        }

        //TODO: make more specific event types

        public static DeviceEvent ThermostatModeChanged(IDevice device, IEventSource source)
        {
            var result = new DeviceEvent(device, new DeviceStateChanged(), source);

            return result;
        }

        public static DeviceEvent ThermostatCurrentActionChanged(IDevice device, IEventSource source)
        {
            var result = new DeviceEvent(device, new DeviceStateChanged(), source);

            return result;
        }

        public static DeviceEvent ThermostatSetpointsChanged(IDevice device, IEventSource source)
        {
            var result = new DeviceEvent(device, new DeviceStateChanged(), source);

            return result;
        }

        public static DeviceEvent ThermostatFanModeChanged(IDevice device, IEventSource source)
        {
            var result = new DeviceEvent(device, new DeviceStateChanged(), source);

            return result;
        }

        public static DeviceEvent ThermostatFanCurrentActionChanged(IDevice device, IEventSource source)
        {
            var result = new DeviceEvent(device, new DeviceStateChanged(), source);

            return result;
        }

        public static DeviceEvent KeypadStateChanged(IDevice device, IEventSource source)
        {
            var result = new DeviceEvent(device, new DeviceStateChanged(), source);

            return result;
        }
    }
}
