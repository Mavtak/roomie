using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class ThermostatModeParameterAttribute : ParameterAttribute
    {
        public ThermostatModeParameterAttribute()
            : base("ThermostatMode", new ThermostatModeParameterType())
        {
        }
    }
}