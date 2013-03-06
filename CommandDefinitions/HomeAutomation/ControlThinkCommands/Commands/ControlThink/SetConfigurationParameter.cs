using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.CommandDefinitions.ZWave.ControlThinkCommands;
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
            var parameterNumber = scope.GetByte("ParameterNumber");
            var value = scope.GetByte("Value");

            var parameter = device.BackingObject.ConfigurationParameters[parameterNumber];

            parameter.Value = value;
        }
    }
}
