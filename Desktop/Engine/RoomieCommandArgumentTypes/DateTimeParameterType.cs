using System;
using System.Text;
using Roomie.Common;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class DateTimeParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "DateTime";

        public string Name
        {
            get
            {
                return Key;
            }
        }

        public bool Validate(string value)
        {
            return TimeUtils.IsDateTime(value);
        }

        public string ValidationMessage(string parameterName = null)
        {
            var builder = new StringBuilder();

            using (new ParameterValidationMessageHelper(builder, parameterName))
            {
                builder.Append("a value that represents aa DateTime, like 1pm, Tuesday at 4:30am, or Weekdays at 5:30pm, or ");

                var anotherTime = DateTime.Now.AddYears(1).AddDays(3);
                builder.Append(anotherTime.Month);
                builder.Append("/");
                builder.Append(anotherTime.Day);
                builder.Append("/");
                builder.Append(anotherTime.Year);
                builder.Append(" 1:23:45am");
            }

            return builder.ToString();
        }
    }
}
