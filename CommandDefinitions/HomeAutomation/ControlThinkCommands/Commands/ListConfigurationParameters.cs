using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.CommandDefinitions.ZWave.ControlThinkCommands;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.ControlThinkCommands.Commands.ControlThink
{
    class GetConfigurationParameters : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device as ZWaveDevice;
            var interpreter = context.Interpreter;

            for (byte i = 0; i < byte.MaxValue; i++)
            {
                ListValue(interpreter, device, i);
            }

            ListValue(interpreter, device, 255);
        }

        private void ListValue(RoomieCommandInterpreter interpreter, ZWaveDevice device, byte i)
        {
            var parameter = device.BackingObject.ConfigurationParameters[i];
            interpreter.WriteEvent(i + ": " + parameter.Value);
        }
    }
}
