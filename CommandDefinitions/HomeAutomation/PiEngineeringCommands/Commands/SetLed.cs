using System;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.PiEngineeringCommands.Commands
{
    //TODO: improve parsing these parameters
    [StringParameter("LED")]
    [StringParameter("Status")]
    [Group("PiEngineering")]
    public class SetLed : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device as PiEngineeringDevice;
            var scope = context.Scope;
            var ledString = scope.ReadParameter("LED").Value;
            var statusString = scope.ReadParameter("Status").Value;

            Led led;
            if (!Enum.TryParse(ledString, out led))
            {
                throw new RoomieRuntimeException("LED \"" + ledString + "\" is not valid");
            }

            LightStatus status;
            if (!Enum.TryParse(statusString, out status))
            {
                throw new RoomieRuntimeException("Status \"" + statusString + "\" is not valid");
            }

            device.SetLeds(led, status);
        }
    }
}
