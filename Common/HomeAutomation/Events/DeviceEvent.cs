﻿using System;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.Common.HomeAutomation.Events
{
    public class DeviceEvent : IDeviceEvent
    {
        public Device Device { get; private set; }
        public IDeviceState State { get; private set; }

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

        private DeviceEvent(Device device, IEventType type, IEventSource source)
        {
            Device = device;
            State = device.Copy();
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

        public static DeviceEvent PoweredOn(Device device, IEventSource source)
        {
            var result = new DeviceEvent(device, new PoweredOn(), source);

            return result;
        }

        public static DeviceEvent PoweredOff(Device device, IEventSource source)
        {
            var result = new DeviceEvent(device, new PoweredOff(), source);

            return result;
        }

        public static DeviceEvent PowerChanged(Device device, IEventSource source)
        {
            var result = new DeviceEvent(device, new DevicePowerChanged(), source);

            return result;
        }

        //TODO: make motion-detector-specific events

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
            var result = new DeviceEvent(device, new TemperatureChanged(), source);

            return result;
        }
    }
}
