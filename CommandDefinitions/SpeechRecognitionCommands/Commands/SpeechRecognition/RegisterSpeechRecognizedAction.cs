using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roomie.CommandDefinitions.SpeechRecognition;
using Roomie.CommandDefinitions.SpeechRecognitionCommands.Attributes;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Commands.SpeechRecognition
{
    public class RegisterSpeechRecognizedAction :SpeechRecognitionCommand
    {
        protected override void Execute_SpeechRecognitionDefinition(SpeechRecognitionCommandContext context)
        {
            var recognizer = context.SpeechRecognizer;
            var command = context.OriginalCommand.InnerCommands;

            recognizer.RegisterSpeechRecognizedAction(command);
        }
    }
}
