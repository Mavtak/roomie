using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class NetworkParameterAttribute : ParameterAttribute
    {
        public NetworkParameterAttribute()
            : base("Network", "String", "<default>")
        { }
    }
}
