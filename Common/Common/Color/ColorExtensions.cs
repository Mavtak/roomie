using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Roomie.Common.Color
{
    public static class ColorExtensions
    {
        //TODO: extension to convert a color to a wavelength measurement

        public static RgbColor FromHexString(string value)
        {
            if (value == null)
            {
                return null;
            }

            if (!value.StartsWith("#"))
            {
                return null;
            }

            if (value.Length != 7)
            {
                return null;
            }

            try
            {
                var red = HexToByte(value.Substring(1, 2));
                var green = HexToByte(value.Substring(3, 2));
                var blue = HexToByte(value.Substring(5, 2));

                var result = new RgbColor(red, green, blue);

                return result;
            }
            catch (FormatException)
            {
                return null;
            }
        }

        private static byte HexToByte(string value)
        {
            var result = byte.Parse(value, NumberStyles.HexNumber);

            return result;
        }

        private static IEnumerable<byte> Colors(this IColor color)
        {
            var rgb = color.RedGreenBlue;

            yield return rgb.Red;
            yield return rgb.Green;
            yield return rgb.Blue;
        }

        public static string ToHexString(this IColor color)
        {
            var result = new StringBuilder();

            result.Append("#");

            foreach (var colorByte in color.Colors())
            {
                result.AppendFormat("{0:X2}", colorByte);
            }

            return result.ToString();
        }
    }
}
