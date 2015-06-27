using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.ControlThinkCommands.Commands
{
    [ByteParameter("ParameterNumber")]
    [ByteParameter("Value")]
    [Group("ControlThink")]
    class SetConfigurationParameter : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device as ZWaveDevice;
            var interpreter = context.Interpreter;
            var scope = context.Scope;
            var parameterNumber = scope.ReadParameter("ParameterNumber").ToByte();
            var value = scope.ReadParameter("Value").ToByte();

            var parameter = device.BackingObject.ConfigurationParameters[parameterNumber];

            parameter.Value = value;
        }
    }
}
