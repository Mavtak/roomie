using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class FanModeParameterAttribute : ParameterAttribute
    {
        public FanModeParameterAttribute()
            : base("FanMode", new FanModeParameterType())
        {
        }
    }
}