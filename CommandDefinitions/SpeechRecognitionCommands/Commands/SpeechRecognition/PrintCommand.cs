using System.Text;
using Roomie.CommandDefinitions.SpeechRecognition;
using Roomie.Desktop.Engine.Commands;
using Roomie.CommandDefinitions.SpeechRecognitionCommands.Attributes;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Commands.SpeechRecognition
{
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
