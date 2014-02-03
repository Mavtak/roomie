
namespace Roomie.Common.HomeAutomation.BinarySwitches
{
    public static class BinarySwitchPowerParser
    {
        public static bool IsValid(string input)
        {
            return EnumParser.IsValid<BinarySwitchPower>(input);
        }

        public static BinarySwitchPower Parse(string input)
        {
            return EnumParser.Parse<BinarySwitchPower>(input);
        }

        public static BinarySwitchPower ToToggleSwitchPower(this string input)
        {
            return Parse(input);
        }
    }
}
