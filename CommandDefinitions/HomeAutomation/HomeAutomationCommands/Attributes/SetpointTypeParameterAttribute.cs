using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class SetpointTypeParameterAttribute : ParameterAttribute
    {
        public SetpointTypeParameterAttribute()
            : base("Setpoint", new SetpointTypeParameterType())
        {
        }
    }
}