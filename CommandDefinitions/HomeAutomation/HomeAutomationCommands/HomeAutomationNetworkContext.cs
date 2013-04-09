using Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class HomeAutomationNetworkContext
    {
        public ThreadPool ThreadPool { get; private set; }
        public IMasterHistory History { get; private set; }
        private readonly RoomieEngine _engine;

        public bool WebHookPresent
        {
            get
            {
                return WebHookConnector.WebHookPresentByCommandLibrary(_engine.CommandLibrary);
            }
        }

        public HomeAutomationNetworkContext(RoomieEngine engine, ThreadPool threadPool, IDeviceHistory deviceHistory, INetworkHistory networkHistory)
        {
            _engine = engine;
            ThreadPool = threadPool;
            //TODO: ninject?
            History = new MasterHistory(deviceHistory, networkHistory);
        }

        public HomeAutomationNetworkContext(RoomieEngine engine, ThreadPool threadPool)
            : this(engine, threadPool, new DeviceHistory(), new NetworkHistory())
        {
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
