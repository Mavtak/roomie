using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class AutoConnectParameterAttribute : ParameterAttribute
    {
        public AutoConnectParameterAttribute()
            : base("AutoConnect", BooleanParameterType.Key, "True")
        { }
    }
}
