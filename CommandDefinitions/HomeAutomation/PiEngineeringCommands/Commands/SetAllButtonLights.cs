using System;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

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
            var scope = context.Scope;
            var bankString = scope.GetValue("Color");
            var power = scope.GetValue("Power").ToBoolean();
            
            Bank bank;
            if (!Enum.TryParse(bankString, out bank))
            {
                throw new RoomieRuntimeException("Color \"" + bankString + "\" is not valid");
            }

            device.SetAllButtonLights(bank, power);
        }
    }
}
