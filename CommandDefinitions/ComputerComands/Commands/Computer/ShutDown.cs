using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.ComputerCommands.Commands.Computer
{
    [Description("This command shuts down the computer")]
    public class ShutDown : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            Common.ShutDownComputer();
        }
    }
}
