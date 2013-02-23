using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine.Commands
{
    public class TimeSpanParameterAttribute : ParameterAttribute
    {
        public TimeSpanParameterAttribute(string name)
            : base(name, TimeSpanParameterType.Key)
        {
        }

        public TimeSpanParameterAttribute(string name, string @default)
            : base(name, TimeSpanParameterType.Key, @default)
        {
        }
    }
}
