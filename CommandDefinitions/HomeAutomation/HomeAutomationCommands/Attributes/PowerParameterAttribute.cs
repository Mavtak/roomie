using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class PowerParameterAttribute : IntegerParameterAttribute
    {
        public PowerParameterAttribute()
            : base("Power")
        { }
    }
}
