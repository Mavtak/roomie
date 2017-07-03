
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public abstract class HomeAutomationCommand : RoomieCommand
    {
        public HomeAutomationCommand()
            : base()
        { }

        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var greaterContext = new HomeAutomationCommandContext(context);

            Execute_HomeAutomationDefinition(greaterContext);
        }

        protected abstract void Execute_HomeAutomationDefinition(HomeAutomationCommandContext context);
    }
}
