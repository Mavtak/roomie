using System.Windows.Forms;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;


namespace Roomie.CommandDefinitions.ComputerCommands.Commands.Computer
{
    [Parameter("Force", "Boolean", "True")]
    [Description("This command hybernates the computer")]
    public class Hibernate : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            bool force = scope.GetBoolean("Force");

            Common.SuspendComputer(PowerState.Hibernate, force);
        }
    }
}
