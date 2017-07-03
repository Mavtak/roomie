using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine.Commands
{
    public class TimeSpanParameterAttribute : ParameterAttribute
    {
        public TimeSpanParameterAttribute(string name)
            : base(name, new TimeSpanParameterType())
        {
        }

        public TimeSpanParameterAttribute(string name, string @default)
            : base(name, new TimeSpanParameterType(), @default)
        {
        }
    }
}
