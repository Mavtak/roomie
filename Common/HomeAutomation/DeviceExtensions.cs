
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
                if (device.ToggleSwitch.IsOn)
                {
                    result = "on";
                }

                if (device.ToggleSwitch.IsOff)
                {
                    result = "off";
                }
            }
            else if (device.Type == DeviceType.Dimmable)
            {
                var percentage = device.DimmerSwitch.Percentage;
                if (percentage != null)
                {
                    result = percentage + "%";
                }
            }
            else if (device.Type == DeviceType.Thermostat)
            {
                var temperature = device.Thermostat.Temperature;
                if (temperature != null)
                {
                    result = temperature.ToString();
                }
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
