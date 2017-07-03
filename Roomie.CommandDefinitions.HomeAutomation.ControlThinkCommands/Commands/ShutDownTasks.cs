using System.Linq;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.ControlThinkCommands.Commands
{
    [Group("ControlThink")]
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
