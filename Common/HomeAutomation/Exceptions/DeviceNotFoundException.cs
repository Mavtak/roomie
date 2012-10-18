﻿
namespace Roomie.Common.HomeAutomation.Exceptions
{
    public class DeviceNotFoundException : HomeAutomationException
    {
        public DeviceNotFoundException(string deviceName)
            : base("Home Automation Device \"" + deviceName + "\" not found.")
        { }

        public DeviceNotFoundException(Device device)
            : this(device.Name)
        { }
    }
}
