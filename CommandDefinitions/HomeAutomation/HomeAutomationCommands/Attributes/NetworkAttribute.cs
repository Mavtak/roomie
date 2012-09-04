using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class NetworkAttribute : ParameterAttribute
    {
        public NetworkAttribute()
            : base("Network", "String", "<default>")
        { }
    }
}
