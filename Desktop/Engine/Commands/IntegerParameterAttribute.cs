using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine.Commands
{
    public class IntegerParameterAttribute : ParameterAttribute
    {
        public IntegerParameterAttribute(string name)
            : base(name, new IntegerParameterType())
        {
        }

        public IntegerParameterAttribute(string name, int @default)
            : base(name, new IntegerParameterType(), @default.ToString())
        {
        }
    }
}
