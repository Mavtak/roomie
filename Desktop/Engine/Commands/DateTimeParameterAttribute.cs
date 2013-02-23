using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine.Commands
{
    public class DateTimeParameterAttribute : ParameterAttribute
    {
        public DateTimeParameterAttribute(string name)
            : base(name, DateTimeParameterType.Key)
        {
        }

        public DateTimeParameterAttribute(string name, string @default)
            : base(name, DateTimeParameterType.Key, @default)
        {
        }
    }
}
