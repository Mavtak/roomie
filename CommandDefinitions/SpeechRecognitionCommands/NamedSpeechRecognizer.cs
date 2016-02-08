using System;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands
{
    public class NamedSpeechRecognizer
    {
        AutomaticallyResettingSwitch _automaticallyResettingSwitch;
        private string _name;
        private PhraseBasedSpeechRecognizer _speechRecognizer;

        public NamedSpeechRecognizer(TimeSpan attentionLength, string name)
        {
            _automaticallyResettingSwitch = new AutomaticallyResettingSwitch(attentionLength);
            _name = name;
            _speechRecognizer = new PhraseBasedSpeechRecognizer();
            _speechRecognizer.RegisterPhrase(name);
        }

        public NamedSpeechRecognizerResponse Recognize(double confidence)
        {
            while (true)
            {
                var input = _speechRecognizer.Recognize(confidence);

                return ProcessInput(input);
            }
        }

        public void RegisterPhrase(string phrase)
        {
            _speechRecognizer.RegisterPhrase(phrase);
        }

        public void UnregisterPhrase(string phrase)
        {
            _speechRecognizer.UnregisterPhrase(phrase);
        }

        private NamedSpeechRecognizerResponse ProcessInput(string input)
        {
            if (input == _name)
            {
                _automaticallyResettingSwitch.SwitchOn();
                return new NamedSpeechRecognizerResponse(input, NamedSpeechRecognizerResponse.StatusProperty.NameRecognized);
            }

            if (!_automaticallyResettingSwitch.IsOn)
            {
                return new NamedSpeechRecognizerResponse(input, NamedSpeechRecognizerResponse.StatusProperty.NameRequired);
            }

            return new NamedSpeechRecognizerResponse(input, NamedSpeechRecognizerResponse.StatusProperty.PhraseRecognized);
        }
    }
}
