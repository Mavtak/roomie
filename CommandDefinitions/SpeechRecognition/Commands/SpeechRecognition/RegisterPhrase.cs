using Roomie.CommandDefinitions.SpeechRecognition;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Commands.SpeechRecognition
{
    [StringParameter("Phrase")]
    public class RegisterPhrase : SpeechRecognitionCommand
    {
        protected override void Execute_SpeechRecognitionDefinition(SpeechRecognitionCommandContext context)
        {
            var phrase = context.Scope.GetValue("Phrase");
            var recognizer = context.SpeechRecognizer;
            var command = context.OriginalCommand.InnerCommands;
            
            recognizer.RegisterPhrase(phrase, command);
        }
    }
}
