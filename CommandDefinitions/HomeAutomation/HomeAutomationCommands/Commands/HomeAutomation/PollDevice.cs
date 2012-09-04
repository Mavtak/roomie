using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [NotFinished]
    public class PollDevice : SingleDeviceControlCommand
    {
        protected override void Execute_HomeAutomationNetwork(HomeAutomationCommandContext context)
        {
            var device = context.Device;

            device.Poll();
        }
    }
}
