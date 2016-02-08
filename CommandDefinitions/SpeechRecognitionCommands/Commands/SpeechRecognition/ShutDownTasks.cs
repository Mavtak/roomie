using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Commands
{
    [Group("SpeechRecognition")]
    public class ShutDownTasks : SpeechRecognitionCommand
    {
        protected override void Execute_SpeechRecognitionDefinition(SpeechRecognitionCommandContext context)
        {
            var interpreter = context.Interpreter;
            var threads = context.Threads;

            interpreter.WriteEvent("Shutting down SpeechRecognition...");

            threads.ShutDown();

            interpreter.WriteEvent("Done shutting down SpeechRecognition.");
        }
    }
}
