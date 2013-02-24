using Roomie.CommandDefinitions.SpeechRecognition;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Commands.SpeechRecognition
{
    public class ListRegisteredPhrases : SpeechRecognitionCommand
    {
        protected override void Execute_SpeechRecognitionDefinition(SpeechRecognitionCommandContext context)
        {
            var recognizer = context.SpeechRecognizer;
            var interpreter = context.Interpreter;

            //TODO: output table
            foreach (var phrase in recognizer.Phrases)
            {
                interpreter.WriteEvent(phrase);
            }
        }
    }
}
