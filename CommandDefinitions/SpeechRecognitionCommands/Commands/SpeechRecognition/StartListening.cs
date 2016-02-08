namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Commands.SpeechRecognition
{
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
