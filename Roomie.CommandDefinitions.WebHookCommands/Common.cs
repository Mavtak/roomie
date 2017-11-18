using System;
using System.Collections.Generic;
using System.Linq;
using Roomie.Common.Api.Models;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;

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

        public static string GetComputerName(DataStore dataStore)
        {
            var engines = GetWebhookEngines(dataStore);

            if (engines.Count == 0)
                throw new RoomieRuntimeException("No WebHook engines present.");

            if (engines.Count > 1)
                throw new RoomieRuntimeException("Multiple WebHook engines not yet supported.");

            var engine = engines.Values.First();

            return engine.ComputerName;
        }

        // Access the dictionary of mailers from the central data store
        public static Dictionary<string, WebHookEngine> GetWebhookEngines(DataStore dataStore)
        {
            var key = typeof(InternalLibraryVersion);
            var value = dataStore.GetAdd<Dictionary<string, WebHookEngine>>(key);
            return value;
        }

        public static Response<TData> Send<TData>(DataStore dataStore, string repository, Request request)
            where TData : class
        {
            var engines = GetWebhookEngines(dataStore);

            if (engines.Count == 0)
                throw new RoomieRuntimeException("No WebHook engines present.");

            if (engines.Count > 1)
                throw new RoomieRuntimeException("Multiple WebHook engines not yet supported.");

            var engine = engines.Values.First();

            return engine.Send<TData>(repository, request);
        }
    }
}
