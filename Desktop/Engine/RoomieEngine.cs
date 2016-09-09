using System;
using System.Collections.Generic;
using Roomie.Desktop.Engine.Delegates;
using Roomie.Desktop.Engine.StreamStorage;

namespace Roomie.Desktop.Engine
{
    public class RoomieEngine
    {
        //TODO: is the access level of GlobalScope appropriate?
        public HierarchicalVariableScope GlobalScope { get; private set; }
        public bool PrintCommandCalls;
        public readonly RoomieCommandLibrary CommandLibrary;
        public readonly bool DevelopmentEnvironment;
        public readonly DataStore DataStore;
        public readonly IStreamStore StreamStore;
        public readonly ArgumentTypeCollection ArgumentTypes;
        private EngineState _engineState;

        //TODO: investigate "public readonly"
        public ThreadPool Threads { get; private set; }
        internal List<ThreadPool> ThreadPools { get; private set; }

        public event RoomieThreadEventHandler ScriptMessageSent;
        public event EngineStateChangedEventHandler EngineStateChanged;

        public RoomieEngine()
        {
            _engineState = EngineState.New;
            GlobalScope = new HierarchicalVariableScope();
            DataStore = new DataStore();
            StreamStore = new BasicStreamStore();
            ThreadPools = new List<ThreadPool>();
            Threads = CreateThreadPool("Root Threads");
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

        public ThreadPool CreateThreadPool(string name)
        {
            var result = new ThreadPool(this, name);
            ThreadPools.Add(result);

            return result;
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
