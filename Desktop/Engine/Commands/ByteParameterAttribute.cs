using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine.Commands
{
    public class ByteParameterAttribute : ParameterAttribute
    {
        public ByteParameterAttribute(string name)
            : base(name, new ByteParameterType())
        {
        }

        public ByteParameterAttribute(string name, string @default)
            : base(name, new ByteParameterType(), @default)
        {
        }
    }
}
