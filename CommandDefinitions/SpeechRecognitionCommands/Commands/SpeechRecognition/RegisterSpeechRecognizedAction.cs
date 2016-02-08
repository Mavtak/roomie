namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Commands.SpeechRecognition
{
    public class RegisterSpeechRecognizedAction :SpeechRecognitionCommand
    {
        protected override void Execute_SpeechRecognitionDefinition(SpeechRecognitionCommandContext context)
        {
            var recognizer = context.SpeechRecognizer;
            var command = context.OriginalCommand.InnerCommands;

            recognizer.RegisterSpeechRecognizedAction(command);
        }
    }
}
