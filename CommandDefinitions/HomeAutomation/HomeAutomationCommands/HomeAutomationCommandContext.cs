
using Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation;
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class HomeAutomationCommandContext : RoomieCommandContext
    {
        public Network Network { get; set; }

        public HomeAutomationCommandContext(RoomieCommandContext context)
            : base(context)
        { }

        public HomeAutomationCommandContext(HomeAutomationCommandContext context)
            : this((RoomieCommandContext) context)
        {
            Network = context.Network;
        }

        public NetworkCollection Networks
        {
            get
            {
                // Access the collection of home automation networks from the central data store
                var key = typeof(InternalLibraryVersion);
                var value = DataStore.GetAdd<NetworkCollection>(key);
                return value;
            }
        }

        public void AddSyncWithCloud()
        {
            var scriptCommand = GetBlankCommand(typeof(SyncWithCloud));

            Interpreter.CommandQueue.AddBeginning(scriptCommand);
        }
    }
}
