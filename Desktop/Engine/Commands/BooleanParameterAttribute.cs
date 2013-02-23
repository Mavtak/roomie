using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine.Commands
{
    public class BooleanParameterAttribute : ParameterAttribute
    {
        public BooleanParameterAttribute(string name)
            : base(name, new BooleanParameterType())
        {
        }

        public BooleanParameterAttribute(string name, bool @default)
            : base(name, new BooleanParameterType(), @default.ToString())
        {
        }
    }
}
