using System.Windows.Forms;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.ComputerCommands.Commands.Computer
{
    [BooleanParameter("Force", true)]
    [Description("This command hybernates the computer")]
    public class Hibernate : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            bool force = context.ReadParameter("Force").ToBoolean();

            Common.SuspendComputer(PowerState.Hibernate, force);
        }
    }
}
