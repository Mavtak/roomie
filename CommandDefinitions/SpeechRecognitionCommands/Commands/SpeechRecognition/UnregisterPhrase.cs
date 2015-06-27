using Roomie.CommandDefinitions.SpeechRecognition;
using Roomie.CommandDefinitions.SpeechRecognitionCommands.Attributes;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Commands.SpeechRecognition
{
    [PhraseParameter]
    public class UnregisterPhrase : SpeechRecognitionCommand
    {
        protected override void Execute_SpeechRecognitionDefinition(SpeechRecognitionCommandContext context)
        {
            var recognizer = context.SpeechRecognizer;
            var phrase = context.Scope.GetValue(PhraseParameterAttribute.Key).Value;

            recognizer.UnregisterPhrase(phrase);
        }
    }
}
