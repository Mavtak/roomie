using System;
using Roomie.Common.HomeAutomation.Exceptions;

namespace Roomie.Common.HomeAutomation
{
    public static class Utilities
    {
        public static bool IsOn(int? power)
        {
            return power != null && !IsOff(power);
        }

        public static bool IsOff(int? power)
        {

            return power == 0; ;
        }

        public static int ValidatePower(int power, int? maxPower)
        {
            if (power < 0)
            {
                throw new HomeAutomationException("Power must be greater than or equal to 0 (attempted value is " + power + ")");
            }

            if (power > maxPower)
            {
                power = maxPower.Value;
            }

            return power;
        }

        public static VirtualAddress ToVirtualAddress(this string value)
        {
            var result = VirtualAddress.Parse(value);

            if (result == null)
            {
                throw new ArgumentException("The input does not represent a valid VirtualAddress", "value");
            }

            return result;
        }
    }
}
