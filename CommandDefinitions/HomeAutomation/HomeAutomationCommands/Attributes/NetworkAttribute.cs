using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class NetworkAttribute : StringParameterAttribute
    {
        public NetworkAttribute()
            : base("Network", "<default>")
        { }
    }
}
