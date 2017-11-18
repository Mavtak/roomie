using System.Linq;
using Roomie.Common.Api.Models;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using WebCommunicator;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    internal static class WebHookConnector
    {
        internal static bool WebHookPresent(RoomieCommandContext context)
        {
            return WebHookPresentByCommandLibrary(context.CommandLibrary);
        }

        internal static bool WebHookPresentByCommandLibrary(RoomieCommandLibrary commandLibrary)
        {
            var result = commandLibrary.ContainsCommandGroup("WebHook");

            return result;
        }

        internal static string GetComputerName(RoomieCommandContext context)
        {
            if (!WebHookPresent(context))
            {
                throw new RoomieRuntimeException("WebHook not present.");
            }

            return WebHookCommands.Common.GetComputerName(context.DataStore);
        }

        internal static Response<TData> Send<TData>(RoomieCommandContext context, string repository, Request request)
            where TData : class
        {
            //TODO: that's not too conclusive. 
            if (!WebHookPresent(context))
            {
                throw new RoomieRuntimeException("WebHook not present.");
            }

            return WebHookCommands.Common.Send<TData>(context.DataStore, repository, request);
        }
    }
}
