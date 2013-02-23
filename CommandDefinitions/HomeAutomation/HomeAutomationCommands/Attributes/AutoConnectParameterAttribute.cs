using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class AutoConnectParameterAttribute : BooleanParameterAttribute
    {
        public AutoConnectParameterAttribute()
            : base("AutoConnect", true)
        { }
    }
}
