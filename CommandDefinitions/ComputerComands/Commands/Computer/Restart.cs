using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.ComputerCommands.Commands.Computer
{
    [Description("This command restarts the computer")]
    public class Restart : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            Common.RestartComputer();
        }
    }
}
