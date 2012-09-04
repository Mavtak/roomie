
namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class WebHookConnectTasks : HomeAutomationNetworkCommand
    {
        protected override void Execute_HomeAutomationNetwork(HomeAutomationCommandContext context)
        {
            context.AddSyncWithCloud();
        }
    }
}
