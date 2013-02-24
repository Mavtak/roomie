using System.Text;
using Roomie.CommandDefinitions.SpeechRecognition;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Commands.SpeechRecognition
{
    [StringParameter("Phrase")]
    public class PrintCommand : SpeechRecognitionCommand
    {
        protected override void Execute_SpeechRecognitionDefinition(SpeechRecognitionCommandContext context)
        {
            var recognizer = context.SpeechRecognizer;
            var interpreter = context.Interpreter;
            var phrase = context.Scope.GetValue("Phrase");
            var commands = recognizer.GetCommand(phrase);
            
            interpreter.WriteEvent(commands.OriginalText);
        }
    }
}
