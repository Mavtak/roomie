using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine.Commands
{
    public class StringParameterAttribute : ParameterAttribute
    {
        public StringParameterAttribute(string name)
            : base(name, StringParameterType.Key)
        {
        }

        public StringParameterAttribute(string name, string @default)
            : base(name, StringParameterType.Key, @default)
        {
        }
    }
}
