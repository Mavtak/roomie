using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine.Commands;

namespace Q42HueCommands.Commands.Q42Hue
{
    [Description("This command polls all lights on the specified network, including ones that were recently added to the Hue bridge.")]
    public class UpdateList : HomeAutomationNetworkCommand
    {
        protected override void Execute_HomeAutomationNetworkDefinition(HomeAutomationCommandContext context)
        {
            var network = context.Network as Q42HueNetwork;

            network.UpdateList();
        }
    }
}
