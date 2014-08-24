using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine.Commands
{
    public class ColorParameterAttribute : ParameterAttribute
    {
        public ColorParameterAttribute(string name)
            : base(name, new ColorParameterType())
        {
        }

        public ColorParameterAttribute(string name, string @default)
            : base(name, new ColorParameterType(), @default)
        {
        }
    }
}
