using System;
using Roomie.Desktop.Engine.Delegates;

namespace Roomie.Desktop.Engine
{
    public class RoomieEngine
    {
        //TODO: is the access level of GlobalScope appropriate?
        public RoomieCommandScope GlobalScope { get; private set; }
        public bool PrintCommandCalls;
        public readonly RoomieCommandLibrary CommandLibrary;
        public readonly bool DevelopmentEnvironment;
        public readonly DataStore DataStore;
        public readonly ArgumentTypeCollection ArgumentTypes;
        private EngineState _engineState;

        //TODO: investigate "public readonly"
        public ThreadPool Threads { get; private set; }

        public event RoomieThreadEventHandler ScriptMessageSent;
        public event EngineStateChangedEventHandler EngineStateChanged;

        public RoomieEngine()
        {
            _engineState = EngineState.New;
            GlobalScope = new RoomieCommandScope();
            DataStore = new DataStore();
            Threads = new ThreadPool(this, "Root Threads");
            CommandLibrary = new RoomieCommandLibrary();
            CommandLibrary.Message += CommandLibrary_Message;
            PrintCommandCalls = false;
            DevelopmentEnvironment = Environment.CurrentDirectory.EndsWith(@"\bin\Debug");
            ArgumentTypes = new ArgumentTypeCollection();
        }

        private void CommandLibrary_Message(object sender, Delegates.RoomieCommandLibraryEventArgs e)
        {
            Threads.Print(e.Message);
        }

        public void Start()
        {
            EngineState = EngineState.Starting;

            var startup = new EngineStartup(this);
            startup.Run();

            EngineState = EngineState.Running;
        }

        public EngineState EngineState
        {
            get
            {
                return _engineState;
            }

            private set
            {
                var oldState = _engineState;
                var newState = value;

                _engineState = newState;

                if (EngineStateChanged != null)
                {
                    var eventArgs = new EngineStateChangedEventArgs(oldState, newState);
                    EngineStateChanged(this, eventArgs);
                }
            }
        }

        internal void SimpleOutputText(RoomieThread thread, string message)
        {
            if (ScriptMessageSent == null)
            {
                throw new Exception("nothing is listening to the engine's output messages!");
            }

            ScriptMessageSent(this, new RoomieThreadEventArgs(thread, message));
        }

        //TODO: add timeout
        public void Shutdown()
        {
            EngineState = EngineState.ShuttingDown;

            var shutdown = new EngineShutdown(this);
            shutdown.Run(() =>
            {
                EngineState = EngineState.ShutDown;
            });
        }

    }
}
