using System;

namespace Roomie.Common.HomeAutomation.Exceptions
{
    [Serializable]
    public class DeviceNotFoundException : HomeAutomationException
    {
        public DeviceNotFoundException(string deviceName)
            : base("Home Automation Device \"" + deviceName + "\" not found.")
        { }

        public DeviceNotFoundException(IDevice device)
            : this(device.Name)
        { }
    }
}
