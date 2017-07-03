using Roomie.CommandDefinitions.SpeechRecognitionCommands.Attributes;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Commands
{
    [Group("SpeechRecognition")]
    [PhraseParameter]
    public class PrintCommand : SpeechRecognitionCommand
    {
        protected override void Execute_SpeechRecognitionDefinition(SpeechRecognitionCommandContext context)
        {
            var recognizer = context.SpeechRecognizer;
            var interpreter = context.Interpreter;
            var phrase = context.ReadParameter(PhraseParameterAttribute.Key).Value;
            var commands = recognizer.GetCommand(phrase);
            
            interpreter.WriteEvent(commands.OriginalText);
        }
    }
}
