using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine.Commands;

namespace Q42HueCommands.Commands
{
    [Description("This command polls all lights on the specified network, including ones that were recently added to the Hue bridge.")]
    [Group("Q42Hue")]
    public class UpdateList : HomeAutomationNetworkCommand
    {
        protected override void Execute_HomeAutomationNetworkDefinition(HomeAutomationCommandContext context)
        {
            var network = context.Network as Q42HueNetwork;

            network.UpdateList();
        }
    }
}
