using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Attributes
{
    public class PhraseParameterAttribute : StringParameterAttribute
    {
        public const string Key = "Phrase";

        public PhraseParameterAttribute()
            : base(Key)
        {
        }

        public PhraseParameterAttribute(string @default)
            : base(Key, @default)
        {
        }
    }
}
