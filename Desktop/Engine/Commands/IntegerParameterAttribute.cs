using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine.Commands
{
    public class IntegerParameterAttribute : ParameterAttribute
    {
        public IntegerParameterAttribute(string name)
            : base(name, IntegerParameterType.Key)
        {
        }

        public IntegerParameterAttribute(string name, string @default)
            : base(name, IntegerParameterType.Key, @default)
        {
        }
    }
}
