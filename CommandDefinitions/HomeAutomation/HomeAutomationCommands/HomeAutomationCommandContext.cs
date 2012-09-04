using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Roomie.Desktop.Engine;
using Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class HomeAutomationCommandContext : RoomieCommandContext
    {
        public Network Network { get; set; }
        public Device Device { get; set; }

        public HomeAutomationCommandContext(RoomieCommandContext context)
            : base(context)
        { }

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
