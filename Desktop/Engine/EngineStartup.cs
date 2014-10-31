using System;
using System.IO;
using Roomie.Common.Exceptions;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine
{
    public class EngineStartup
    {
        private const string StartupScriptPath = "Startup.RoomieScript";
        private readonly RoomieEngine _engine;
        private readonly ThreadPool _startupThreads;

        public EngineStartup(RoomieEngine engine)
        {
            _engine = engine;
            _startupThreads = engine.CreateThreadPool("Startup Threads");
        }

        public void Run()
        {
            _startupThreads.Print("Roomie Engine started.  VROOOOOM! :D");

            SetInitialVariables();
            LoadCommands();
            LoadArgumentTypes();
            InitializePlugins();
            RunUserStartupScript();
        }

        private void SetInitialVariables()
        {
            if (_engine.DevelopmentEnvironment)
            {
                Print("mmm, I'm in development now.  Be gentle. :'3");
            }

            _engine.GlobalScope.DeclareVariable("Name", "Roomie");
            _engine.GlobalScope.DeclareVariable("Version", Common.LibraryVersion.ToString());
            _engine.GlobalScope.DeclareVariable("Started", DateTime.Now.ToString());
        }

        private void LoadCommands()
        {
            _engine.CommandLibrary.AddCommandsFromAssembly(GetType().Assembly);
            _engine.CommandLibrary.AddCommandsFromPluginFolder(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void LoadArgumentTypes()
        {
            //TODO: reflect for types
            _engine.ArgumentTypes.Add(new BooleanParameterType());
            _engine.ArgumentTypes.Add(new ByteParameterType());
            _engine.ArgumentTypes.Add(new DateTimeParameterType());
            _engine.ArgumentTypes.Add(new IntegerParameterType());
            //TODO: what about IntergerRangeParameterType?
            _engine.ArgumentTypes.Add(new StringParameterType());
            _engine.ArgumentTypes.Add(new TimeSpanParameterType());
        }

        private void InitializePlugins()
        {
            foreach (var command in _engine.CommandLibrary.StartupTasks)
            {
                var newThread = _startupThreads.CreateNewThread(command.ExtensionName + " Plugin Startup");

                newThread.AddCommand(command.BlankCommandCall());
            }
        }

        private void RunUserStartupScript()
        {
            //TODO: add a startup script search if DevelopmentEnvironment.
            Print("Searching in \"" + Environment.CurrentDirectory + "\" for \"" + StartupScriptPath + "\"");
            if (File.Exists(StartupScriptPath))
            {
                try
                {
                    var script = RoomieScript.FromFile(StartupScriptPath);
                    _startupThreads.AddCommands(script);
                }
                catch (RoomieRuntimeException exception)
                {
                    Print("I had trouble loading the startup script: " + exception.Message);
                }
            }
            else
            {
                //TODO: add a utility function for building single commands
                Print("No Startup Script Found.  Create 'Startup.RoomieScript' in the working directory to use this feature.");
            }
        }

        private void Print(string text)
        {
            _startupThreads.Print(text);
        }
    }
}
