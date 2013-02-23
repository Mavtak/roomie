using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine.Commands
{
    public class BooleanParameterAttribute : ParameterAttribute
    {
        public BooleanParameterAttribute(string name)
            : base(name, BooleanParameterType.Key)
        {
        }

        public BooleanParameterAttribute(string name, bool @default)
            : base(name, BooleanParameterType.Key, @default.ToString())
        {
        }
    }
}
