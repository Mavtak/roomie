using System;
using Roomie.Common.Exceptions;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine
{
    public class RoomieEngine
    {
        private const string StartupScriptPath = "Startup.RoomieScript";

        //TODO: is the access level of GlobalScope appropriate?
        public RoomieCommandScope GlobalScope { get; private set; }
        public bool PrintCommandCalls;
        public readonly RoomieCommandLibrary CommandLibrary;
        public readonly bool DevelopmentEnvironment;
        public readonly DataStore DataStore;
        public readonly ArgumentTypeCollection ArgumentTypes;

        private RoomieThread _rootThread;

        //TODO: investigate "public readonly"
        public ThreadPool Threads { get; private set; }
        
        public event RoomieThreadEventHandler ScriptMessageSent;

        public RoomieEngine()
        {
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
            //TODO: catch more exceptions
            RootThread.WriteEvent("Roomie Engine started.  VROOOOOM! :D");

            if (DevelopmentEnvironment)
            {
                RootThread.WriteEvent("mmm, I'm in development now.  Be gentle. :'3");
            }

            GlobalScope.DeclareVariable("Name", "Roomie");
            GlobalScope.DeclareVariable("Version", Common.LibraryVersion.ToString());
            GlobalScope.DeclareVariable("Started", DateTime.Now.ToString());
            
            //load core commands
            CommandLibrary.AddCommandsFromAssembly(GetType().Assembly);

            //load plugins
            CommandLibrary.AddCommandsFromPluginFolder(AppDomain.CurrentDomain.BaseDirectory);

            //run startup tasks
            foreach (var command in CommandLibrary.StartupTasks)
            {
                var newThread = Threads.CreateNewThread(command.ExtensionName + " Plugin Startup");

                newThread.AddCommand(command.BlankCommandCall());
            }

            //run user's startup script

            //TODO: add a startup script search if DevelopmentEnvironment.

            RootThread.WriteEvent("Searching in \"" + Environment.CurrentDirectory + "\" for \"" + StartupScriptPath + "\"");
            if (System.IO.File.Exists(StartupScriptPath))
            {
                try
                {
                    var script = RoomieScript.FromFile(StartupScriptPath);
                    Threads.AddCommands(script);
                }
                catch (RoomieRuntimeException exception)
                {
                    RootThread.WriteEvent("I had trouble loading the startup script: " + exception.Message);
                }
            }
            else
            {
                //TODO: add a utility function for building single commands
                RootThread.WriteEvent("No Startup Script Found.  Create 'Startup.RoomieScript' in the working directory to use this feature.");
            }
        }

        #region accessors

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
            var thread = new System.Threading.Thread(new System.Threading.ThreadStart(shutdown_helper));
            thread.Start();
        }
        private void shutdown_helper()
        {
            Threads.ShutDown(); //TODO: what about other thread pools?
            var shutdownThreads = new ThreadPool(this, "Shutdown Tasks");

            foreach (var command in CommandLibrary.ShutDownTasks)
            {
                _rootThread.WriteEvent("Calling " + command.FullName);

                shutdownThreads.AddCommand(command.BlankCommandCall());
            }

            //TODO: fix this
            var killTime = DateTime.Now.AddSeconds(30);
            //while(shutdownThreads.IsBusy && DateTime.Now<=killTime)
            //{
            //    System.Threading.Thread.Sleep(100);
            //}
            System.Threading.Thread.Sleep(10000);

            //if (shutdownThreads.IsBusy)
            shutdownThreads.ShutDown();

            RootThread.WriteEvent("Done.");

            //this thread will die when the function returns, and all threads will be killed.
        }
    }
}
