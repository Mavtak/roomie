﻿using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.ControlThinkCommands.Commands
{
    [ByteParameter("ParameterNumber")]
    [Group("ControlThink")]
    class GetConfigurationParameter : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device as ZWaveDevice;
            var interpreter = context.Interpreter;
            var scope = context.Scope;
            var parameterNumber = scope.ReadParameter("ParameterNumber").ToByte();

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
