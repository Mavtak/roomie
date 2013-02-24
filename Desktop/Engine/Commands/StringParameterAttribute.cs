using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine.Commands
{
    public class StringParameterAttribute : ParameterAttribute
    {
        public StringParameterAttribute(string name)
            : base(name, new StringParameterType())
        {
        }

        public StringParameterAttribute(string name, string @default)
            : base(name, new StringParameterType(), @default)
        {
        }
    }
}
