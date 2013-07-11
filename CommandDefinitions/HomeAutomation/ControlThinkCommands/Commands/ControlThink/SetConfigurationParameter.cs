using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.ControlThinkCommands.Commands.ControlThink
{
    [ByteParameter("ParameterNumber")]
    [ByteParameter("Value")]
    class SetConfigurationParameter : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device as ZWaveDevice;
            var interpreter = context.Interpreter;
            var scope = context.Scope;
            var parameterNumber = scope.GetValue("ParameterNumber").ToByte();
            var value = scope.GetValue("Value").ToByte();

            var parameter = device.BackingObject.ConfigurationParameters[parameterNumber];

            parameter.Value = value;
        }
    }
}
