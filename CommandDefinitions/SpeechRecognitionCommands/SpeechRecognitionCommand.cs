using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.SpeechRecognition
{
    public abstract class SpeechRecognitionCommand : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var greaterContext = new SpeechRecognitionCommandContext(context);

            Execute_SpeechRecognitionDefinition(greaterContext);
        }

        protected abstract void Execute_SpeechRecognitionDefinition(SpeechRecognitionCommandContext context);

    }
}
