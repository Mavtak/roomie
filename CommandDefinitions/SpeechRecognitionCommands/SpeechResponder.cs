using System;
using System.Collections.Generic;
using System.Threading;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine;
using ThreadPool = Roomie.Desktop.Engine.ThreadPool;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands
{
    public class SpeechResponder
    {
        private Dictionary<string, ScriptCommandList> RegisteredCommands { get; set; }
        private ThreadPool ThreadPool { get; set; }
        private NamedSpeechRecognizer _speechRecognizer { get; set; }
        private Thread _recognitionThread;
        private ScriptCommandList _speechRecognizedAction;

        public SpeechResponder(RoomieEngine engine)
        {
            RegisteredCommands = new Dictionary<string, ScriptCommandList>();
            _speechRecognizer = new NamedSpeechRecognizer(TimeSpan.FromSeconds(10), "Roomie");
            ThreadPool = engine.CreateThreadPool("Speech Recognizer");
        }

        public void StartListening()
        {
            //TODO: prevent from this running more than once
            lock (this)
            {
                if (_recognitionThread == null)
                {
                    var start = new ThreadStart(StartListening);
                    _recognitionThread = new Thread(start);
                    _recognitionThread.Start();

                    return;
                }
            }

            while (true)
            {
                var input = _speechRecognizer.Recognize(0.8);

                ProcessInput(input);
            }
        }

        public void StopListening()
        {
            lock (this)
            {
                if (_recognitionThread != null)
                {
                    _recognitionThread.Abort();
                    _recognitionThread = null;
                }
            }
        }

        public void RegisterPhrase(string phrase, ScriptCommandList command)
        {
            RegisteredCommands.Add(phrase, command);
            _speechRecognizer.RegisterPhrase(phrase);
            
        }

        public void UnregisterPhrase(string phrase)
        {
            _speechRecognizer.UnregisterPhrase(phrase);
            RegisteredCommands.Remove(phrase);
        }

        public void ProcessInput(NamedSpeechRecognizerResponse input)
        {
            switch (input.Status)
            {
                case NamedSpeechRecognizerResponse.StatusProperty.NameRecognized:
                    ThreadPool.AddCommands(new TextScriptCommand("Computer.Speak Text=\"I'm ready.\""));
                    break;

                case NamedSpeechRecognizerResponse.StatusProperty.NameRequired:
                    ThreadPool.AddCommands(new TextScriptCommand("Computer.Speak Text=\"What was that?\""));
                    break;

                case NamedSpeechRecognizerResponse.StatusProperty.PhraseRecognized:
                    if (_speechRecognizedAction != null)
                    {
                        ThreadPool.AddCommands(_speechRecognizedAction);
                    }
                    var command = GetCommand(input.Phrase);

                    ThreadPool.AddCommands(command);
                    break;
            }
            
        }

        public IEnumerable<string> Phrases
        {
            get
            {
                return RegisteredCommands.Keys;
            }
        }

        public ScriptCommandList GetCommand(string phrase)
        {
            return RegisteredCommands[phrase];
        }

        public void RegisterSpeechRecognizedAction(ScriptCommandList commands)
        {
            lock (this)
            {
                _speechRecognizedAction = new ScriptCommandList();
                _speechRecognizedAction.Add(commands);
            }
        }
    }
}
