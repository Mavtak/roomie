
namespace Roomie.Common.HomeAutomation.ToggleSwitches
{
    public static class ToggleSwitchPowerParser
    {
        public static bool IsValid(string input)
        {
            return EnumParser.IsValid<ToggleSwitchPower>(input);
        }

        public static ToggleSwitchPower Parse(string input)
        {
            return EnumParser.Parse<ToggleSwitchPower>(input);
        }

        public static ToggleSwitchPower ToToggleSwitchPower(this string input)
        {
            return Parse(input);
        }
    }
}
