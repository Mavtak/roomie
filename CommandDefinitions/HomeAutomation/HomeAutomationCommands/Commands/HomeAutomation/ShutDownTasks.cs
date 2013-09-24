
namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class ShutDownTasks : HomeAutomationCommand
    {
        protected override void Execute_HomeAutomationDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var threads = context.ThreadPool;

            interpreter.WriteEvent("shutting down Home Automation...");

            threads.ShutDown();

            var subinterpreter = interpreter.GetSubinterpreter();
            var saveDataScriptCommand = context.GetBlankCommand(typeof(SaveData));
            subinterpreter.CommandQueue.Add(saveDataScriptCommand);
            subinterpreter.ProcessQueue();

            interpreter.WriteEvent("done shutting down home automation.");
        }
    }
}
