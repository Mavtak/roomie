using Roomie.Common.HomeAutomation;
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class DeviceAddressParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "DeviceAddress";

        public string Name
        {
            get { return Key; }
        }

        public bool Validate(string value)
        {
            var result = VirtualAddress.IsValid(value);

            return result;
        }
    }
}