using System;
using System.IO;
using Roomie.Common.Exceptions;
using Roomie.Common.ScriptingLanguage;

namespace Roomie.Desktop.Engine
{
    public class EngineStartup
    {
        private const string StartupScriptPath = "Startup.RoomieScript";
        private readonly RoomieEngine _engine;

        public EngineStartup(RoomieEngine engine)
        {
            _engine = engine;
        }

        public void Run()
        {
            _engine.RootThread.WriteEvent("Roomie Engine started.  VROOOOOM! :D");

            SetInitialVariables();
            LoadCommands();
            InitializePlugins();
            RunUserStartupScript();
        }

        private void SetInitialVariables()
        {
            if (_engine.DevelopmentEnvironment)
            {
                _engine.RootThread.WriteEvent("mmm, I'm in development now.  Be gentle. :'3");
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

        private void InitializePlugins()
        {
            foreach (var command in _engine.CommandLibrary.StartupTasks)
            {
                var newThread = _engine.Threads.CreateNewThread(command.ExtensionName + " Plugin Startup");

                newThread.AddCommand(command.BlankCommandCall());
            }
        }

        private void RunUserStartupScript()
        {
            //TODO: add a startup script search if DevelopmentEnvironment.
            _engine.RootThread.WriteEvent("Searching in \"" + Environment.CurrentDirectory + "\" for \"" + StartupScriptPath + "\"");
            if (File.Exists(StartupScriptPath))
            {
                try
                {
                    var script = RoomieScript.FromFile(StartupScriptPath);
                    _engine.Threads.AddCommands(script);
                }
                catch (RoomieRuntimeException exception)
                {
                    _engine.RootThread.WriteEvent("I had trouble loading the startup script: " + exception.Message);
                }
            }
            else
            {
                //TODO: add a utility function for building single commands
                _engine.RootThread.WriteEvent("No Startup Script Found.  Create 'Startup.RoomieScript' in the working directory to use this feature.");
            }
        }
    }
}
