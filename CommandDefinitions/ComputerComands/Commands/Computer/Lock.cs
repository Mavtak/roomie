using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.ComputerCommands.Commands.Computer
{
    [Description("This command locks the computer")]
    public class Lock : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            Common.LockComputer();
        }
    }
}
