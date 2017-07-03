using System;
using Q42.HueApi;
using Q42.HueApi.ColorConverters;
using Q42.HueApi.ColorConverters.OriginalWithModel;
using Roomie.Common.Color;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Utilities = Roomie.Common.HomeAutomation.Utilities;

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

        public static IColor CalculateColor(Light light)
        {
            var hexColor = "#" + light.ToHex();
            var result = hexColor.ToColor();

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

        public static LightCommand CreateCommand(IColor color, Light light)
        {
            var rgb = color.RedGreenBlue;

            var result = new LightCommand()
                .SetColor(new RGBColor(rgb.Red, rgb.Green, rgb.Blue), light.ModelId);

            return result;
        }
    }
}
