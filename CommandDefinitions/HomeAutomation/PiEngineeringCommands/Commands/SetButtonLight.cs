using System;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.PiEngineeringCommands.Commands
{
    //TODO: improve parsing these parameters
    [StringParameter("Color")]
    [StringParameter("Button")]
    [StringParameter("Status")]
    [Group("PiEngineering")]
    public class SetButtonLight : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device as PiEngineeringDevice;
            var scope = context.Scope;
            var bankString = scope.GetValue("Color").Value;
            var button = scope.GetValue("Button").Value;
            var statusString = scope.GetValue("Status").Value;

            Bank bank;
            if (!Enum.TryParse(bankString, out bank))
            {
                throw new RoomieRuntimeException("Color \"" + bankString + "\" is not valid");
            }

            var column = (byte) (button[0] - 'A');
            var row = (byte) (button[1] - '1');

            LightStatus status;
            if (!Enum.TryParse(statusString, out status))
            {
                throw new RoomieRuntimeException("Status \"" + statusString + "\" is not valid");
            }

            device.SetButtonLight(bank, row, column, status);
        }
    }
}
