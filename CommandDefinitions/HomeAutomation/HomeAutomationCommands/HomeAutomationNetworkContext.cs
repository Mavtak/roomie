using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roomie.Desktop.Engine;
using Roomie.Common.ScriptingLanguage;
using Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class HomeAutomationNetworkContext
    {
        public ThreadPool ThreadPool { get; set; }
        private RoomieEngine _engine;

        public bool WebHookPresent
        {
            get
            {
                return WebHookConnector.WebHookPresentByCommandLibrary(_engine.CommandLibrary);
            }
        }

        public HomeAutomationNetworkContext(RoomieEngine engine, ThreadPool threadPool)
        {
            _engine = engine;
            ThreadPool = threadPool;
        }

        public IScriptCommand SyncWithCloudCommand
        {
            get
            {
                var result = _engine.CommandLibrary.GetCommandFromType(typeof(SyncWithCloud)).BlankCommandCall();

                return result;
            }
        }
    }
}
