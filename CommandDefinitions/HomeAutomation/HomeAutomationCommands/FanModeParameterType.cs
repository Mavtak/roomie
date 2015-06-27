using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Parameters;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;
using System.Text;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class FanModeParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "FanMode";

        public string Name
        {
            get { return Key; }
        }

        public bool Validate(IParameter parameter)
        {
            return parameter.IsThermostatFanMode();
        }

        public string ValidationMessage(string parameterName = null)
        {
            var builder = new StringBuilder();

            //TODO: list all programatically
            using (new ParameterValidationMessageHelper(builder, parameterName))
            {
                builder.Append("a value that represents a fan mode, like On or Auto.");
            }

            return builder.ToString();
        }
    }
}
