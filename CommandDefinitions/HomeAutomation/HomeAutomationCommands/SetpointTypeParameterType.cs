using System.Text;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Parameters;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class SetpointTypeParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "SetpointType";

        public string Name
        {
            get { return Key; }
        }

        public bool Validate(IParameter parameter)
        {
            var result = ThermostatSetpointTypeParser.IsValid(parameter.Value);

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
