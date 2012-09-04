using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class PowerParameterAttribute : ParameterAttribute
    {
        public PowerParameterAttribute()
            : base("Power", "Integer")
        { }
    }
}
