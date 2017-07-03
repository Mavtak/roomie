using System.Text;
using Roomie.Common.Measurements.Ratio;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class HumidityParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "Humidity";

        public string Name
        {
            get
            {
                return Key;
            }
        }

        public bool Validate(IParameter parameter)
        {
            return RatioParser.IsRatio(parameter.Value);
        }

        public string ValidationMessage(string parameterName = null)
        {
            var builder = new StringBuilder();

            using (new ParameterValidationMessageHelper(builder, parameterName))
            {
                builder.Append("a value that represents a humidity, like 100%, 0%, or 53%");
            }

            return builder.ToString();
        }
    }
}
