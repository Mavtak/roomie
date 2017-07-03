using System;
using System.Collections.Generic;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using WebCommunicator;

namespace Roomie.CommandDefinitions.WebHookCommands
{
    public static class Common
    {
        public static Version LibraryVersion
        {
            get
            {
                return InternalLibraryVersion.GetLibraryVersion();
            }
        }

        // Access the dictionary of mailers from the central data store
        public static Dictionary<string, WebHookEngine> GetWebhookEngines(DataStore dataStore)
        {
            var key = typeof(InternalLibraryVersion);
            var value = dataStore.GetAdd<Dictionary<string, WebHookEngine>>(key);
            return value;
        }

        public static Message SendMessage(DataStore dataStore, Message outMessage)
        {
            var engines = GetWebhookEngines(dataStore);

            if (engines.Count == 0)
                throw new RoomieRuntimeException("No WebHook engines present.");

            if (engines.Count > 1)
                throw new RoomieRuntimeException("Multiple WebHook engines not yet supported.");

            WebHookEngine engine = null;

            //TODO: I know, I know... this is awful.
            foreach (WebHookEngine firstEngine in engines.Values)
                engine = firstEngine;

            return engine.SendMessage(outMessage);
        }
    }
}
