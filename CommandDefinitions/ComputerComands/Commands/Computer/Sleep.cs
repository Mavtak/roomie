using System.Windows.Forms;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;


namespace Roomie.CommandDefinitions.ComputerCommands.Commands.Computer
{
    [BooleanParameter("Force", "True")]
    [Description("This command sleeps the computer.")]
    public class Sleep : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            bool force = scope.GetBoolean("Force");

            Common.SuspendComputer(PowerState.Suspend, force);
        }
    }
}
