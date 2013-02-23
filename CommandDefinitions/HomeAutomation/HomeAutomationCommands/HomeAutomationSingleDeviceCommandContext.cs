
using Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation;
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class HomeAutomationSingleDeviceContext : HomeAutomationCommandContext
    {
        public Device Device { get; set; }

        public HomeAutomationSingleDeviceContext(RoomieCommandContext context)
            : base(context)
        { }
    }
}
