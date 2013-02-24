using Roomie.CommandDefinitions.SpeechRecognition;
using Roomie.CommandDefinitions.SpeechRecognitionCommands.Attributes;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Commands.SpeechRecognition
{
    [PhraseParameter]
    public class RegisterPhrase : SpeechRecognitionCommand
    {
        protected override void Execute_SpeechRecognitionDefinition(SpeechRecognitionCommandContext context)
        {
            var phrase = context.Scope.GetValue(PhraseParameterAttribute.Key);
            var recognizer = context.SpeechRecognizer;
            var command = context.OriginalCommand.InnerCommands;
            
            recognizer.RegisterPhrase(phrase, command);
        }
    }
}
