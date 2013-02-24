using System.Text;
using Roomie.Common.HomeAutomation;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

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

        public string ValidationMessage(string parameterName = null)
        {
            var builder = new StringBuilder();

            using (new ParameterValidationMessageHelper(builder, parameterName))
            {
                //TODO: make these real examples!
                builder.Append("a value that represents a device address, like \"Ceiling Light\", \"Living Room: Ceiling Light\", \"[12]\", or \"home network/Living Room: Ceiling Light\"");
            }

            return builder.ToString();
        }
    }
}