using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [NotFinished]
    public class PollDevices : HomeAutomationNetworkCommand
    {
        protected override void Execute_HomeAutomationNetworkDefinition(HomeAutomationCommandContext context)
        {
            var network = context.Network;

            foreach (var device in network.Devices)
            {
                device.Poll();
            }
        }
    }
}
