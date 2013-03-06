using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.ControlThinkCommands.Commands.ControlThink
{
    [ByteParameter("ParameterNumber")]
    class GetConfigurationParameter : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device as ZWaveDevice;
            var interpreter = context.Interpreter;
            var scope = context.Scope;
            var parameterNumber = scope.GetByte("ParameterNumber");

            var parameter = device.BackingObject.ConfigurationParameters[parameterNumber];

            if (parameter == null)
            {
                interpreter.WriteEvent("Parameter does not exist");
                return;
            }

            interpreter.WriteEvent(parameter.Value.ToString());
        }
    }
}
