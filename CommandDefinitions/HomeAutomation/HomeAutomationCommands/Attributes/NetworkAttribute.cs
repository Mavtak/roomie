using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class NetworkAttribute : StringParameterAttribute
    {
        public NetworkAttribute()
            : base("Network", "<default>")
        { }
    }
}
