using System.Diagnostics;
using System.Linq;
using System.Threading;
using Roomie.Common;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

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
            var name = context.ReadParameter("Name");
            var expectedRunning = context.ReadParameter("Running").ToBoolean();
            var pollInterval = context.ReadParameter("PollInterval").ToTimeSpan();

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
