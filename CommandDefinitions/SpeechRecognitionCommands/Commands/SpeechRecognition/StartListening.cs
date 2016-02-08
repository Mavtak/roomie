using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Commands
{
    [Group("SpeechRecognition")]
    public class StartListening : SpeechRecognitionCommand
    {
        protected override void Execute_SpeechRecognitionDefinition(SpeechRecognitionCommandContext context)
        {
            var recognizer = context.SpeechRecognizer;
            var interpreter = context.Interpreter;

            recognizer.StartListening();
            interpreter.WriteEvent("Speech recognition enabled");
        }
    }
}
