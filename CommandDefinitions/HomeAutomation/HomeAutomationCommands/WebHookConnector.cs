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

        internal static Message SendMessage(RoomieCommandContext context, Message outMessage)
        {
            //TODO: that's not too conclusive. 
            if (!WebHookPresent(context))
            {
                throw new RoomieRuntimeException("WebHook not present.");
            }

            Message response = WebHookCommands.Common.SendMessage(context.DataStore, outMessage);

            return response;
        }
    }
}
