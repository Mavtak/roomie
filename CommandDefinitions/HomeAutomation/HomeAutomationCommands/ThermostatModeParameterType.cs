using System.Text;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Parameters;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class ThermostatModeParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "ThermostatMode";

        public string Name
        {
            get { return Key; }
        }

        public bool Validate(IParameter parameter)
        {
            return parameter.IsThermostatMode();
        }

        public string ValidationMessage(string parameterName = null)
        {
            var builder = new StringBuilder();

            //TODO: list all programatically
            using (new ParameterValidationMessageHelper(builder, parameterName))
            {
                builder.Append("a value that represents a thermostat mode, like Heat, Cool, Auto, FanOnly, or Off.");
            }

            return builder.ToString();
        }
    }
}
