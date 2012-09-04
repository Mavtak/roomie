using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [NotFinished]
    public class PollDevices : HomeAutomationNetworkCommand
    {
        protected override void Execute_HomeAutomationNetwork(HomeAutomationCommandContext context)
        {
            var network = context.Network;

            foreach (Device device in network.Devices)
            {
                device.Poll();
            }
        }
    }
}
