using Roomie.CommandDefinitions.SpeechRecognitionCommands.Attributes;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Commands
{
    [Group("SpeechRecognition")]
    [PhraseParameter]
    public class RegisterPhrase : SpeechRecognitionCommand
    {
        protected override void Execute_SpeechRecognitionDefinition(SpeechRecognitionCommandContext context)
        {
            var phrase = context.ReadParameter(PhraseParameterAttribute.Key).Value;
            var recognizer = context.SpeechRecognizer;
            var command = context.OriginalCommand.InnerCommands;
            
            recognizer.RegisterPhrase(phrase, command);
        }
    }
}
