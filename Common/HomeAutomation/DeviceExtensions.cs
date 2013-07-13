using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.Common.HomeAutomation
{
    public static class DeviceExtensions
    {
        public static string BuildStatus(this Device device)
        {
            string result = null;

            if (device.Type == DeviceType.Switch || device.Type == DeviceType.MotionDetector)
            {
                //TODO: account for motion detectors specifically
                result = device.ToggleSwitch.Describe();
            }
            else if (device.Type == DeviceType.Dimmable)
            {
                result = device.DimmerSwitch.Describe();
            }
            else if (device.Type == DeviceType.Thermostat)
            {
                result = device.Thermostat.Describe();
            }
            else if (!device.Type.CanControl || !device.Type.CanPoll)
            {
                result = "n/a";
            }

            if (result == null)
            {
                result = "?";
            }

            if (device.IsConnected != true)
            {
                result += "?";
            }

            return result;
        }
    }
}
