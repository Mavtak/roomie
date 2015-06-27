using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class NetworkParameterAttribute : StringParameterAttribute
    {
        public NetworkParameterAttribute()
            : base("Network", "<default>")
        { }
    }
}
