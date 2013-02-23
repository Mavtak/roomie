using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class DeviceParameterAttribute : ParameterAttribute
    {
        //TODO: validation on an address?
        public DeviceParameterAttribute()
            : base("Device", StringParameterType.Key)
        { }
    }
}
