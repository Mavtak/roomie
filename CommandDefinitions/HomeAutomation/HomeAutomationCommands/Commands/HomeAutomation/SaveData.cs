
namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class SaveData : HomeAutomationCommand
    {
        protected override void Execute_HomeAutomationDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var networks = context.Networks;

            foreach (Network network in networks)
            {
                interpreter.WriteEvent("Saving " + network.Name);
                network.Save();
            }
        }
    }
}
