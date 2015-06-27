using System.Windows.Forms;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.ComputerCommands.Commands.Computer
{
    [BooleanParameter("Force", true)]
    [Description("This command sleeps the computer.")]
    public class Sleep : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            bool force = context.ReadParameter("Force").ToBoolean();

            Common.SuspendComputer(PowerState.Suspend, force);
        }
    }
}
