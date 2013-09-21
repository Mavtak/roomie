
namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class HomeAutomationSingleDeviceContext : HomeAutomationCommandContext
    {
        public Device Device { get; set; }

        public HomeAutomationSingleDeviceContext(HomeAutomationCommandContext context)
            : base(context)
        { }
    }
}
