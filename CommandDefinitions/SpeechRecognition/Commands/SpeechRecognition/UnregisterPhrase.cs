using Roomie.CommandDefinitions.SpeechRecognition;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Commands.SpeechRecognition
{
    [Parameter("Phrase", "String")]
    public class UnregisterPhrase : SpeechRecognitionCommand
    {
        protected override void Execute_SpeechRecognitionDefinition(SpeechRecognitionCommandContext context)
        {
            var recognizer = context.SpeechRecognizer;
            var phrase = context.Scope.GetValue("phrase");

            recognizer.UnregisterPhrase(phrase);
        }
    }
}
