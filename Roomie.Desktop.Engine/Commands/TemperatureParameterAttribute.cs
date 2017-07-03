using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine.Commands
{
    public class TemperatureParameterAttribute : ParameterAttribute
    {
        public TemperatureParameterAttribute(string name)
            : base(name, new TemperatureParameterType())
        {
        }

        public TemperatureParameterAttribute(string name, string @default)
            : base(name, new TemperatureParameterType(), @default)
        {
        }
    }
}
