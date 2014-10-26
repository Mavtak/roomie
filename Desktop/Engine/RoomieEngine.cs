using System;
using Roomie.Common.Exceptions;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine.Delegates;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

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

        private RoomieThread _rootThread;

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
            
            //TODO: reflect for types
            ArgumentTypes.Add(new BooleanParameterType());
            ArgumentTypes.Add(new ByteParameterType());
            ArgumentTypes.Add(new DateTimeParameterType());
            ArgumentTypes.Add(new IntegerParameterType());
            //TODO: what about IntergerRangeParameterType?
            ArgumentTypes.Add(new StringParameterType());
            ArgumentTypes.Add(new TimeSpanParameterType());
        }

        /// <summary>
        /// event handler for CommandLibrary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandLibrary_Message(object sender, Delegates.RoomieCommandLibraryEventArgs e)
        {
            SimpleOutputText(RootThread, e.Message);
        }

        public void Start()
        {
            EngineState = EngineState.Starting;

            var startup = new EngineStartup(this);
            startup.Run();

            EngineState = EngineState.Running;
        }

        #region accessors

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

        public RoomieThread RootThread
        {
            get
            {
                lock (this)
                {
                    if (_rootThread == null)
                    {
                        _rootThread = Threads.CreateNewThread("Root Thread");
                    }

                    return _rootThread;
                }
            }
        }

        #endregion

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
            var thread = new System.Threading.Thread(shutdown_helper);
            thread.Start();
        }
        private void shutdown_helper()
        {
            EngineState = EngineState.ShuttingDown;

            RootThread.WriteEvent("Shutting Down...");

            Threads.ShutDown(); //TODO: what about other thread pools?
            var shutdownThreads = new ThreadPool(this, "Shutdown Tasks");

            foreach (var command in CommandLibrary.ShutDownTasks)
            {
                _rootThread.WriteEvent("Calling " + command.FullName);

                shutdownThreads.AddCommands(command.BlankCommandCall());
            }

            //TODO: fix this
            var killTime = DateTime.Now.AddSeconds(10);
            while(shutdownThreads.IsBusy && DateTime.Now<=killTime)
            {
                System.Threading.Thread.Sleep(100);
            }

            shutdownThreads.ShutDown();

            RootThread.WriteEvent("Done.");

            EngineState = EngineState.ShutDown;

            //this thread will die when the function returns, and all threads will be killed.
        }
    }
}
