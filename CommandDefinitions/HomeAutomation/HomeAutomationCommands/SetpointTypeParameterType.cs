using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;
using System.Text;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class SetpointTypeParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "SetpointType";

        public string Name
        {
            get { return Key; }
        }

        public bool Validate(string value)
        {
            var result = SetpointTypeParser.IsValid(value);

            return result;
        }

        public string ValidationMessage(string parameterName = null)
        {
            var builder = new StringBuilder();

            //TODO: list all programatically
            using (new ParameterValidationMessageHelper(builder, parameterName))
            {
                builder.Append("a value that represents a setpoint, like Heat or Cool.");
            }

            return builder.ToString();
        }
    }
}
