using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.PiEngineeringCommands.Commands
{
    [StringParameter("Address", "PiEngineeringDevices")]
    [Description("This command attempts to connect to P. I. Engineering devices.")]
    [Group("PiEngineering")]
    public class RegisterNetwork : HomeAutomationCommand
    {
        protected override void Execute_HomeAutomationDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var networks = context.Networks;

            var address = context.ReadParameter("Address").Value;

            interpreter.WriteEvent("Searching for PiEngineering devices");

            var network = new PiEngineeringNetwork(new HomeAutomationNetworkContext(context.Engine, context.ThreadPool), address);
            networks.Add(network);

            interpreter.WriteEvent("Done.");
        }
    }
}
