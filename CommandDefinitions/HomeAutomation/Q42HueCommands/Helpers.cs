using System;
using Q42.HueApi;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySwitches;

namespace Q42HueCommands
{
    public static class Helpers
    {
        public static byte MaxPower = 254;

        public static int CalculatePower(Light light)
        {
            var state = light.State;

            var result = state.Brightness;

            if (state.On == false)
            {
                result = 0;
            }

            return result;
        }

        public static LightCommand CreateCommand(int power)
        {
            power = Utilities.ValidatePower(power, MaxPower);

            var result = new LightCommand();

            var on = power > 0;

            result.On = on;

            if (on)
            {
                result.Brightness = (byte)power;
            }

            return result;
        }

        public static LightCommand CreateCommand(BinarySwitchPower power)
        {
            var result = new LightCommand();

            switch (power)
            {
                case BinarySwitchPower.On:
                    result.On = true;
                    result.Brightness = byte.MaxValue;
                    break;

                case BinarySwitchPower.Off:
                    result.On = false;
                    result.Brightness = byte.MinValue;
                    break;

                default:
                    throw new Exception();
            }

            return result;
        }
    }
}
