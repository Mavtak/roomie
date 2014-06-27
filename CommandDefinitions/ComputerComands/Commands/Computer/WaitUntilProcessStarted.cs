using System.Diagnostics;
using System.Linq;
using System.Threading;
using Roomie.Common;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.ComputerCommands.Commands.Computer
{
    [StringParameter("Name")]
    [BooleanParameter("Running", true)]
    [TimeSpanParameter("PollInterval", "500ms")]
    [Description("Wait until a process is runnning or not running.")]
    public class WaitUntilProcessRunning : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;
            var name = scope.GetValue("Name");
            var expectedRunning = scope.GetValue("Running").ToBoolean();
            var pollInterval = scope.GetValue("PollInterval").ToTimeSpan();

            while (true)
            {
                var processes = Process.GetProcesses();
                var actualRunning = processes.Any(x => string.Equals(x.ProcessName, name));

                if (actualRunning == expectedRunning)
                {
                    return;
                }

                Thread.Sleep(pollInterval);
            }
        }
    }
}
