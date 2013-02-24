using System;
using System.Speech.Synthesis;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.ComputerCommands.Commands.Computer
{
    [StringParameter("Text")]
    [BooleanParameter("Async", false)]
    [Description("This command uses speach synthesis to translate the given text to audio.")]
    public sealed class Speak : RoomieCommand, IDisposable
    {
        private SpeechSynthesizer synthesizer;
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            if (synthesizer == null)
            {
                synthesizer = new SpeechSynthesizer();
            }

            String text = scope.GetValue("Text");
            bool async = scope.GetBoolean("Async");


            if (async)
                synthesizer.SpeakAsync(text);
            else
                synthesizer.Speak(text);
        }

        public void Dispose()
        {
            synthesizer.Dispose();
        }
    }
}
