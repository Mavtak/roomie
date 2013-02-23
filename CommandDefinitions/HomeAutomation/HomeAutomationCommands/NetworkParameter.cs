using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class NetworkParameterAttribute : ParameterAttribute
    {
        public NetworkParameterAttribute()
            : base("Network", StringParameterType.Key, "<default>")
        { }
    }
}
