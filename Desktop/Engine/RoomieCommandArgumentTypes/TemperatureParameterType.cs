using System;
using System.Text;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class TemperatureParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "Temperature";

        public string Name
        {
            get
            {
                return Key;
            }
        }

        public bool Validate(string value)
        {
            return TemperatureParser.IsTemperature(value);
        }

        public string ValidationMessage(string parameterName = null)
        {
            var builder = new StringBuilder();

            using (new ParameterValidationMessageHelper(builder, parameterName))
            {
                builder.Append("a value that represents a temperatureTime, like 20C, 70 Fahrenheit, or 200.5 Kelvin");
            }

            return builder.ToString();
        }
    }
}
