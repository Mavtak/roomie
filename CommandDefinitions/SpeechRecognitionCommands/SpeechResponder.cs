using Roomie.Common.ScriptingLanguage;
using System.Collections.Generic;
using System.Speech.Recognition;
using System.Threading;
using Roomie.Desktop.Engine;
using ThreadPool = Roomie.Desktop.Engine.ThreadPool;

namespace Roomie.CommandDefinitions.SpeechRecognition
{
    public class SpeechResponder
    {
        private Dictionary<string, ScriptCommandList> RegisteredCommands { get; set; }
        private SpeechRecognitionEngine SpeechRecognizer { get; set; }
        private ThreadPool ThreadPool { get; set; }
        private Thread _recognitionThread;
        private ScriptCommandList _speechRecognizedAction;

        public SpeechResponder(RoomieEngine engine)
        {
            RegisteredCommands = new Dictionary<string, ScriptCommandList>();
            SpeechRecognizer = new SpeechRecognitionEngine();
            ThreadPool = new ThreadPool(engine, "Speech Recognizer");

            SpeechRecognizer.SetInputToDefaultAudioDevice();
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
                var result = SpeechRecognizer.Recognize();

                if (result == null)
                {
                    //TODO: logic to ignore babble
                    ThreadPool.Print("heard something that I didn't recognize...");
                    Thread.Sleep(500);
                }
                else
                {
                    var phrase = result.Text;

                    ProcessPhrase(phrase);
                }
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

            var grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(phrase);

            var grammar = new Grammar(grammarBuilder);

            SpeechRecognizer.LoadGrammar(grammar);
        }

        public void UnregisterPhrase(string phrase)
        {
            RegisteredCommands.Remove(phrase);
        }

        public void ProcessPhrase(string phrase)
        {
            if (_speechRecognizedAction != null)
            {
                ThreadPool.AddCommands(_speechRecognizedAction);
            }
            var command = GetCommand(phrase);

            ThreadPool.AddCommands(command);
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

        public void EmulateRecognize(string phrase)
        {
            SpeechRecognizer.EmulateRecognize(phrase);
        }

        public void RemovePhrase(string phrase)
        {
            RegisteredCommands.Remove(phrase);
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
