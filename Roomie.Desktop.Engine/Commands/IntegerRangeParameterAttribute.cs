using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine.Commands
{
    public class IntegerRangeParameterAttribute : ParameterAttribute
    {
        public IntegerRangeParameterAttribute(string name, int min, int max)
            : base(name, new IntegerRangeParameterType(min, max))
        {
        }

        public IntegerRangeParameterAttribute(string name, int min, int max, int @default)
            : base(name, new IntegerRangeParameterType(min, max), @default.ToString())
        {
        }
    }
}
