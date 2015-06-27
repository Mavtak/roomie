﻿using Roomie.CommandDefinitions.HomeAutomationCommands.Attributes;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [PowerParameter]
    public class Dim : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var scope = context.Scope;
            var device = context.Device;

            var power = scope.ReadParameter("Power").ToInteger();

            device.MultilevelSwitch.SetPower(power);
        }
    }
}
