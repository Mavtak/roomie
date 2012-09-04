
namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class ShutDownTasks : HomeAutomationCommand
    {
        protected override void Execute_HomeAutomation(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;

            interpreter.WriteEvent("shutting down Home Automation...");

            var subinterpreter = interpreter.GetSubinterpreter();

            var saveDataScriptCommand = context.GetBlankCommand(typeof(SaveData));
            subinterpreter.CommandQueue.Add(saveDataScriptCommand);
            subinterpreter.ProcessQueue();

            interpreter.WriteEvent("done shutting down home automation.");
        }
    }
}
