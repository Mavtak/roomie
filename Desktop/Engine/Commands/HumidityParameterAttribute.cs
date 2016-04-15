using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine.Commands
{
    public class HumidityParameterAttribute : ParameterAttribute
    {
        public HumidityParameterAttribute(string name)
            : base(name, new HumidityParameterType())
        {
        }

        public HumidityParameterAttribute(string name, string @default)
            : base(name, new HumidityParameterType(), @default)
        {
        }
    }
}
