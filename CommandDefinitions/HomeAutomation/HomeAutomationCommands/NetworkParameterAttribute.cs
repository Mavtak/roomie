using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class NetworkParameterAttribute : StringParameterAttribute
    {
        public NetworkParameterAttribute()
            : base("Network", "<default>")
        { }
    }
}
