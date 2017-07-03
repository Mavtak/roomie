using Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.ScriptingLanguage;
using Roomie.Common.Triggers;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.StreamStorage;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class HomeAutomationNetworkContext
    {
        public ThreadPool ThreadPool { get; private set; }
        public IMasterHistory History { get; private set; }
        public IStreamStore StreamStore => _engine.StreamStore;
        public ITriggerCollection Triggers { get; private set; }
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
            Triggers = new TriggerCollection();
        }

        public HomeAutomationNetworkContext(RoomieEngine engine, ThreadPool threadPool)
            : this(engine, threadPool, new DeviceHistory(), new NetworkHistory())
        {
        }

        public IScriptCommand SyncWithCloudCommand(Network network)
        {
            var commandName = _engine.CommandLibrary.GetCommandFromType(typeof (SyncWithCloud)).BlankCommandCall().OriginalText;
            var parameters = " Network=\"" + network.Address + "\"";

            var result = new TextScriptCommand(commandName + parameters);

            return result;
        }
    }
}
