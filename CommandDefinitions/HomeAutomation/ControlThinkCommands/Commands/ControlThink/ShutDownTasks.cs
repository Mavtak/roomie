using System.Linq;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.ControlThinkCommands.Commands.ControlThink
{
    public class ShutDownTasks : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var homeAutomationContext = new HomeAutomationCommandContext(context);
            var zWaveNetworks = homeAutomationContext.Networks.OfType<ZWaveNetwork>();
            var workQueues = zWaveNetworks.Select(x => x.WorkQueue);

            foreach (var workQueue in workQueues)
            {
                workQueue.ShutDown();
            }
        }
    }
}
