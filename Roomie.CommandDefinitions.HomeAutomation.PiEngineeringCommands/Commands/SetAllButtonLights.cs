﻿using System;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.PiEngineeringCommands.Commands
{
    [StringParameter("Color")]
    [BooleanParameter("Power")]
    [Group("PiEngineering")]
    public class SetAllButtonLights : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device as PiEngineeringDevice;
            var bankString = context.ReadParameter("Color").Value;
            var power = context.ReadParameter("Power").ToBoolean();
            
            Bank bank;
            if (!Enum.TryParse(bankString, out bank))
            {
                throw new RoomieRuntimeException("Color \"" + bankString + "\" is not valid");
            }

            device.SetAllButtonLights(bank, power);
        }
    }
}
