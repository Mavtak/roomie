using System.Text;
using Roomie.Common;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class TimeSpanParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "TimeSpan";

        public string Name
        {
            get
            {
                return Key;
            }
        }

        public bool Validate(string value)
        {
            return TimeUtils.IsTimeSpan(value);
        }

        public string ValidationMessage(string parameterName = null)
        {
            var builder = new StringBuilder();

            using (new ParameterValidationMessageHelper(builder, parameterName))
            {
                builder.Append("a value that represents a Time Span, like 1s, 5 minutes, or 1 day 1 ms");
            }

            return builder.ToString();
        }
    }
}
