namespace Roomie.CommandDefinitions.SpeechRecognition
{
    public class NamedSpeechRecognizerResponse
    {
        public string Phrase { get; private set; }
        public StatusProperty Status { get; private set; }

        public NamedSpeechRecognizerResponse(string phrase, StatusProperty status)
        {
            Phrase = phrase;
            Status = status;
        }

        public enum StatusProperty
        {
            NameRecognized,
            NameRequired,
            PhraseRecognized,
        }
    }
}
