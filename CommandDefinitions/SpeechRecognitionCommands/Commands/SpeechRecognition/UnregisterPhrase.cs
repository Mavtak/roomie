using Roomie.CommandDefinitions.SpeechRecognitionCommands.Attributes;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Commands.SpeechRecognition
{
    [PhraseParameter]
    public class UnregisterPhrase : SpeechRecognitionCommand
    {
        protected override void Execute_SpeechRecognitionDefinition(SpeechRecognitionCommandContext context)
        {
            var recognizer = context.SpeechRecognizer;
            var phrase = context.ReadParameter(PhraseParameterAttribute.Key).Value;

            recognizer.UnregisterPhrase(phrase);
        }
    }
}
