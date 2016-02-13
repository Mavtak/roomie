using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.WeMoCommands.Commands
{
    [StringParameter("IP")]
    [Group("WeMo")]
    public class ConnectDevice : HomeAutomationNetworkCommand
    {
        protected override void Execute_HomeAutomationNetworkDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var network = context.Network as WeMoNetwork;
            var ip = context.ReadParameter("IP").Value;

            network.Connect(ip);
        }
    }
}
