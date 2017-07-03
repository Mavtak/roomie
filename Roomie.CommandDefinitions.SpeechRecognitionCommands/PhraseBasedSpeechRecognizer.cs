using System.Collections.Generic;
using System.Speech.Recognition;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands
{
    public class PhraseBasedSpeechRecognizer
    {
        private Dictionary<string, Grammar> _grammars;
        private SpeechRecognitionEngine _speechRecognizer;

        public PhraseBasedSpeechRecognizer()
        {
            _grammars = new Dictionary<string, Grammar>();
            _speechRecognizer = new SpeechRecognitionEngine();

            _speechRecognizer.SetInputToDefaultAudioDevice();
        }

        public string Recognize(double confidence)
        {
            while (true)
            {
                var result = _speechRecognizer.Recognize();

                if (result != null && result.Confidence >= confidence)
                {
                    return result.Text;
                }
            }
        }

        public void RegisterPhrase(string phrase)
        {
            var grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(phrase);

            var grammar = new Grammar(grammarBuilder);

            _grammars.Add(phrase, grammar);
            _speechRecognizer.LoadGrammar(grammar);

        }

        public void UnregisterPhrase(string phrase)
        {
            var grammar = _grammars[phrase];

            _speechRecognizer.UnloadGrammar(grammar);
            _grammars.Remove(phrase);
        }
    }
}
