using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class AutoConnectParameterAttribute : ParameterAttribute
    {
        public AutoConnectParameterAttribute()
            : base("AutoConnect", "Boolean", "True")
        { }
    }
}
